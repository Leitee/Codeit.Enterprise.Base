/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions.Desentralized
{
    using RabbitMQ.Client;
    using System;

    public interface IRabbitMQPersistentConnection
        : IDisposable
    {
        int RetryCount { get; }

        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
