using System;
using NServiceBus;

namespace Sales.Messages.Commands
{
    public class RegisterDeal : IMessage
    {
        public Guid CorrelationId { get; set; }
        public Guid DealId { get; set; }
        public Guid LeadId { get; set; } 
        public int Value { get; set; }
    }
}
