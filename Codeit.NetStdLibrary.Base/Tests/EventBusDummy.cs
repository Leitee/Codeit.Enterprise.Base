using Microsoft.Extensions.Logging;
using Codeit.NetStdLibrary.Base.Abstractions.Desentralized;
using System;

namespace Codeit.NetStdLibrary.Base.Tests
{
    /// <summary>
    /// To fake proccessing of events. 
    /// </summary>
    public class EventBusDummy : IEventBus
    {
        ILogger<EventBusDummy> _logger;

        public EventBusDummy(ILoggerFactory loggerFactory)
        {
            _logger = (loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory))).CreateLogger<EventBusDummy>();
        }

        public void Publish(IntegrationEventPayload @event)
        {
            _logger.LogDebug($"Publishing integration event: {@event.Id} {Newtonsoft.Json.JsonConvert.SerializeObject(@event)}");
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEventPayload
            where TH : IIntegrationEventHandler<T>
        {
            _logger.LogDebug($"Suscribing handler {typeof(TH).Name} to integration event {typeof(T).Name}");
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            _logger.LogDebug($"Suscribing dynamically handler {typeof(TH).Name} to event {eventName}");
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEventPayload
            where TH : IIntegrationEventHandler<T>
        {
            _logger.LogDebug($"Unsuscribing handler {typeof(TH).Name} with integration event {typeof(T).Name}");
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            _logger.LogDebug($"Unsuscribing dynamically handler {typeof(TH).Name} with event {eventName}");
        }
    }
}
