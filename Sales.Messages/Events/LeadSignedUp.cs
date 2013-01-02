using System;
using NServiceBus;

namespace Sales.Messages.Events
{
    public class LeadSignedUp : IEvent
    {
        public Guid CorrelationId { get; set; }
        public Guid LeadId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string PhoneNumber { get; set; }
    }
}
