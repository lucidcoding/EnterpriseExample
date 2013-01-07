using System;
using NServiceBus.Saga;

namespace Finance.MessageHandlers.SagaData
{
    public class OpenAccountSagaData : ISagaEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Originator { get; set; }
        public virtual string OriginalMessageId { get; set; }
        public virtual bool DealRegisteredReceived { get; set; }
        public virtual bool AgreementActivatedReceived { get; set; }
        public virtual Guid DealId { get; set; }
        public virtual Guid AgreementId { get; set; }
        public virtual Guid ClientId { get; set; }
        public virtual DateTime Commencement { get; set; }
        public virtual DateTime Expiry { get; set; }
        public virtual int Value { get; set; }
    }
}
