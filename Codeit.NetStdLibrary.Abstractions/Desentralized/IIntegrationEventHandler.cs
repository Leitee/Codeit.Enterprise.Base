/// <summary>
/// 
/// </summary>
namespace Codeit.NetStdLibrary.Base.Abstractions.Desentralized
{
    using System.Threading.Tasks;

    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEventPayload
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
