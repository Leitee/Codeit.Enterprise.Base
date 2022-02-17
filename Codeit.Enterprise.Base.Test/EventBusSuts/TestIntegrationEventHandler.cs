using Codeit.Enterprise.Base.Abstractions.Desentralized;
using System.Threading.Tasks;

namespace Codeit.Enterprise.Base.Test.EventBusSuts
{
    public class TestIntegrationEventHandler : IIntegrationEventHandler<TestIntegrationEvent>
    {
        public bool Handled { get; private set; }

        public TestIntegrationEventHandler()
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
