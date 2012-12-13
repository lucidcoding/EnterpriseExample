using System;
using NServiceBus;

namespace ClientServices.Messages.Events
{
    public class AgreementActivated : IEvent
    {
        public Guid Id { get; set; }
        public DateTime Commencement { get; set; }
        public DateTime Expiry { get; set; }
        public int Value { get; set; }
    }
}
