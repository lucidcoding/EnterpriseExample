using System;
using NServiceBus;

namespace Sales.Messages.Commands
{
    public class BookVisit : IMessage
    {
        public Guid Id { get; set; }
        public Guid LeadId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid? ConsultantId { get; set; }
    }
}
