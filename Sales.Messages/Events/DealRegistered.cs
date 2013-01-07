using System;
using NServiceBus;

namespace Sales.Messages.Events
{
    public class DealRegistered : IEvent
    {
        public Guid Id { get; set; }
        public Guid LeadId { get; set; }
        public Guid MadeByConsultantId { get; set; }
        public int Value { get; set; }
        public int Commission { get; set; }
    }
}
