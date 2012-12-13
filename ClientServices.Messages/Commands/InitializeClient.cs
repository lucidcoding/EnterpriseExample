using System;
using System.Collections.Generic;
using NServiceBus;

namespace ClientServices.Messages.Commands
{
    public class InitializeClient : IMessage
    {
        public Guid ClientId { get; set; }
        public Guid AgreementId { get; set; }
        public DateTime AgreementCommencement { get; set; }
        public DateTime AgreementExpiry { get; set; }
        public int AgreementValue { get; set; }
        public IList<Guid> AgreementServiceIds { get; set; }
    }
}
