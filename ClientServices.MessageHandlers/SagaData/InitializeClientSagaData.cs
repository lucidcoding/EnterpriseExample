using System;
using System.Collections.Generic;
using NServiceBus.Saga;

namespace ClientServices.MessageHandlers.SagaData
{
    public class InitializeClientSagaData : ISagaEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Originator { get; set; }
        public virtual string OriginalMessageId { get; set; }
        public virtual Guid CorrelationId { get; set; }
        public virtual bool LeadSignedUpReceived { get; set; }
        public virtual bool InitializeClientReceived { get; set; }
        public virtual Guid ClientId { get; set; }
        public virtual string ClientName { get; set; }
        public virtual string ClientAddress1 { get; set; }
        public virtual string ClientAddress2 { get; set; }
        public virtual string ClientAddress3 { get; set; }
        public virtual string ClientPhoneNumber { get; set; }
        public virtual string ClientEmailAddress { get; set; }
        public virtual Guid DealId { get; set; }
        public virtual DateTime AgreementCommencement { get; set; }
        public virtual DateTime AgreementExpiry { get; set; }
        public virtual int AgreementValue { get; set; }
        public virtual IList<Guid> AgreementServiceIds { get; set; }
    }
}
