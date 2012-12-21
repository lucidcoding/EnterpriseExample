using ClientServices.Messages.Events;
using NServiceBus;

namespace Finance.MessageHandlers.Core
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
            _bus.Subscribe<AgreementActivated>();
            _bus.Subscribe<AgreementCancelled>();
        }

        public void Stop()
        {
            _bus.Unsubscribe<AgreementActivated>();
            _bus.Unsubscribe<AgreementCancelled>();
        }
    }
}
