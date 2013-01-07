using System;
using NServiceBus;

namespace ClientServices.Messages.Events
{
    public class AgreementActivated : IEvent
    {
        public Guid Id { get; set; }
        public Guid DealId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime Commencement { get; set; }
        public DateTime Expiry { get; set; }
    }
}
