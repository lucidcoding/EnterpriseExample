using System;
using System.Collections.Generic;
using NServiceBus;

namespace Sales.Messages.Commands
{
    public class RegisterDeal : IMessage
    {
        public Guid Id { get; set; }
        public Guid LeadId { get; set; } 
        public int Value { get; set; }
    }
}
