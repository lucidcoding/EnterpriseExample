using System;
using NServiceBus;

namespace Sales.Messages.Commands
{
    public class LogVisit : IMessage
    {
        public Guid Id { get; set; }
        public Guid LeadId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
