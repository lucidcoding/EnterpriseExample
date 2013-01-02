using Finance.Messages.Events;
using NServiceBus;
using HumanResources.Messages.Events;
using Sales.Messages.Events;

namespace ClientServices.MessageHandlers.Core
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
            _bus.Subscribe<EmployeeLeft>();
            _bus.Subscribe<AccountSuspended>();
            _bus.Subscribe<LeadSignedUp>();
        }

        public void Stop()
        {
            _bus.Unsubscribe<EmployeeLeft>();
            _bus.Unsubscribe<AccountSuspended>();
            _bus.Unsubscribe<LeadSignedUp>();
        }
    }
}
