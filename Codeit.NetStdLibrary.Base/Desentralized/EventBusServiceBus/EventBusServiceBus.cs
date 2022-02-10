
namespace Codeit.NetStdLibrary.Base.Desentralized.EventBusServiceBus
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;
    using Codeit.NetStdLibrary.Base.Abstractions.Desentralized;
    using Codeit.NetStdLibrary.Base.Desentralized.EventBus;
    using System;
    using System.Threading.Tasks;
    using Azure.Messaging.ServiceBus;
    using System.Text;
    using Azure.Messaging.ServiceBus.Administration;
    using Newtonsoft.Json;

    public class EventBusServiceBus : IEventBus
    {
        private readonly IServiceBusPersisterConnection _serviceBusPersisterConnection;
        private readonly ILogger<EventBusServiceBus> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly string _topicName = "codeit_event_bus";
        private readonly string _subscriptionName;
        private ServiceBusSender _sender;
        private ServiceBusProcessor _processor;
        private readonly IServiceProvider _serviceProvider;
        private const string INTEGRATION_EVENT_SUFFIX = "IntegrationEvent";

        public EventBusServiceBus(IServiceBusPersisterConnection serviceBusPersisterConnection,
                                  ILogger<EventBusServiceBus> logger,
                                  IEventBusSubscriptionsManager subsManager,
                                  string subscriptionClientName,
                                  IServiceProvider serviceProvider)
        {
            _serviceBusPersisterConnection = serviceBusPersisterConnection;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            _serviceProvider = serviceProvider;
            _subscriptionName = subscriptionClientName;
            _sender = _serviceBusPersisterConnection.TopicClient.CreateSender(_topicName);
            ServiceBusProcessorOptions options = new ServiceBusProcessorOptions { MaxConcurrentCalls = 10, AutoCompleteMessages = false };
            _processor = _serviceBusPersisterConnection.TopicClient.CreateProcessor(_topicName, _subscriptionName, options);

            RemoveDefaultRule();
            RegisterSubscriptionClientMessageHandlerAsync().GetAwaiter().GetResult();
        }

        public void Publish(IntegrationEventPayload @event)
        {
            var eventName = @event.GetType().Name.Replace(INTEGRATION_EVENT_SUFFIX, "");
            var jsonMessage = System.Text.Json.JsonSerializer.Serialize(@event, @event.GetType());
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            var message = new ServiceBusMessage
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = new BinaryData(body),
                Subject = eventName,
            };

            _sender.SendMessageAsync(message)
                .GetAwaiter()
                .GetResult();
        }

        public void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(TH).Name);

            _subsManager.AddDynamicSubscription<TH>(eventName);
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEventPayload
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name.Replace(INTEGRATION_EVENT_SUFFIX, "");

            var containsKey = _subsManager.HasSubscriptionsForEvent<T>();
            if (!containsKey)
            {
                try
                {
                    _serviceBusPersisterConnection.AdministrationClient.CreateRuleAsync(_topicName, _subscriptionName, new CreateRuleOptions
                    {
                        Filter = new CorrelationRuleFilter() { Subject = eventName },
                        Name = eventName
                    }).GetAwaiter().GetResult();
                }
                catch (ServiceBusException)
                {
                    _logger.LogWarning("The messaging entity {eventName} already exists.", eventName);
                }
            }

            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).Name);

            _subsManager.AddSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _logger.LogInformation("Unsubscribing from dynamic event {EventName}", eventName);

            _subsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEventPayload
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name.Replace(INTEGRATION_EVENT_SUFFIX, "");

            try
            {
                _serviceBusPersisterConnection
                    .AdministrationClient
                    .DeleteRuleAsync(_topicName, _subscriptionName, eventName)
                    .GetAwaiter()
                    .GetResult();
            }
            catch (ServiceBusException ex) when (ex.Reason == ServiceBusFailureReason.MessagingEntityNotFound)
            {
                _logger.LogWarning("The messaging entity {eventName} Could not be found.", eventName);
            }

            _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

            _subsManager.RemoveSubscription<T, TH>();
        }

        public void Dispose()
        {
            _subsManager.Clear();
            _processor.CloseAsync().GetAwaiter().GetResult();
        }

        private async Task RegisterSubscriptionClientMessageHandlerAsync()
        {
            _processor.ProcessMessageAsync +=
                async (args) =>
                {
                    var eventName = $"{args.Message.Subject}{INTEGRATION_EVENT_SUFFIX}";
                    string messageData = args.Message.Body.ToString();

                    // Complete the message so that it is not received again.
                    if (await ProcessEvent(eventName, messageData))
                    {
                        await args.CompleteMessageAsync(args.Message);
                    }
                };

            _processor.ProcessErrorAsync += ErrorHandler;
            await _processor.StartProcessingAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            var ex = args.Exception;
            var context = args.ErrorSource;

            _logger.LogError(ex, "ERROR handling message: {ExceptionMessage} - Context: {@ExceptionContext}", ex.Message, context);

            return Task.CompletedTask;
        }

        private void RemoveDefaultRule()
        {
            try
            {
                _serviceBusPersisterConnection
                    .AdministrationClient
                    .DeleteRuleAsync(_topicName, _subscriptionName, RuleProperties.DefaultRuleName)
                    .GetAwaiter()
                    .GetResult();
            }
            catch (ServiceBusException ex) when (ex.Reason == ServiceBusFailureReason.MessagingEntityNotFound)
            {
                _logger.LogWarning("The messaging entity {DefaultRuleName} Could not be found.", RuleProperties.DefaultRuleName);
            }
        }

        private async Task<bool> ProcessEvent(string eventName, string message)
        {
            var processed = false;
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        if (subscription.IsDynamic)
                        {
                            var handler = scope.ServiceProvider.GetRequiredService(subscription.HandlerType) as IDynamicIntegrationEventHandler;
                            if (handler == null) continue;
                            dynamic eventData = JObject.Parse(message);
                            await handler.Handle(eventData);
                        }
                        else
                        {
                            var handler = scope.ServiceProvider.GetRequiredService(subscription.HandlerType);
                            if (handler == null) continue;
                            var eventType = _subsManager.GetEventTypeByName(eventName);
                            var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                        }
                    }
                }
                processed = true;
            }
            return processed;
        }
    }
}
