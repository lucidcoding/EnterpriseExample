using NServiceBus;
using Sales.Messages.Events;

namespace ItOps.MessageHandlers.Core
{
    public class SubscriberEndpoint : IWantToRunAtStartup
    {
        private readonly IBus _bus;

        public SubscriberEndpoint(IBus bus)
        {
            _bus = bus;
        }

        public void Run()
        {
            _bus.Subscribe<LeadsUnassigned>();
        }

        public void Stop()
        {
            _bus.Unsubscribe<LeadsUnassigned>();
        }
    }
}
