/// <summary>
/// 
/// </summary>
namespace Codeit.Enterprise.Base.Abstractions.Desentralized
{
    using System.Threading.Tasks;

    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
