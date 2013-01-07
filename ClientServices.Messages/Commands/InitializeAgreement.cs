using System;
using System.Collections.Generic;
using NServiceBus;

namespace ClientServices.Messages.Commands
{
    public class InitializeAgreement : IMessage
    {
        public Guid CorrelationId { get; set; }
        public Guid ClientId { get; set; }
        public Guid DealId { get; set; }
        public DateTime Commencement { get; set; }
        public DateTime Expiry { get; set; }
        public int Value { get; set; }
        public IList<Guid> ServiceIds { get; set; }
    }
}
