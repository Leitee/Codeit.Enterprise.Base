using Codeit.NetStdLibrary.Base.Abstractions.Desentralized;
using System.Threading.Tasks;

namespace Tests.EventBus
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
