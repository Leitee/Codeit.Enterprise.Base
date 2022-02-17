using Codeit.Enterprise.Base.Abstractions.Desentralized;
using System.Threading.Tasks;

namespace Codeit.Enterprise.Base.Test.EventBusSuts
{
    public class TestIntegrationOtherEventHandler : IIntegrationEventHandler<TestIntegrationEvent>
    {
        public bool Handled { get; private set; }

        public TestIntegrationOtherEventHandler()
        {
            Handled = false;
        }

        public async Task Handle(TestIntegrationEvent @event)
        {
            Handled = true;
            await Task.CompletedTask;
        }
    }
}
