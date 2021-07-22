using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Codeit.NetStdLibrary.Base.Abstractions.Desentralized;
using System;

namespace Codeit.NetStdLibrary.Base.Desentralized.EventBusServiceBus
{
    public class DefaultServiceBusPersisterConnection : IServiceBusPersisterConnection
    {
        private readonly ILogger<DefaultServiceBusPersisterConnection> _logger;
        private readonly ServiceBusConnectionStringBuilder _serviceBusConnectionStringBuilder;
        private ITopicClient _topicClient;
        bool _disposed;

        public DefaultServiceBusPersisterConnection(ServiceBusConnectionStringBuilder serviceBusConnectionStringBuilder,
            ILoggerFactory loggerFactory)
        {
            _logger = (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger<DefaultServiceBusPersisterConnection>();

            _serviceBusConnectionStringBuilder = serviceBusConnectionStringBuilder ??
                throw new ArgumentNullException(nameof(serviceBusConnectionStringBuilder));
            _topicClient = new TopicClient(_serviceBusConnectionStringBuilder, RetryPolicy.Default);
        }

        public ServiceBusConnectionStringBuilder ServiceBusConnectionStringBuilder => _serviceBusConnectionStringBuilder;

        public ITopicClient CreateModel()
        {
            if (_topicClient.IsClosedOrClosing)
            {
                _topicClient = new TopicClient(_serviceBusConnectionStringBuilder, RetryPolicy.Default);
            }

            return _topicClient;
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _topicClient.CloseAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error at disposing {connection}", typeof(DefaultServiceBusPersisterConnection));
            }

        }
    }
}
