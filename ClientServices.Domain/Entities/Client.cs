using System;
using System.Collections.Generic;
using ClientServices.Domain.Common;
using ClientServices.Domain.Events;

namespace ClientServices.Domain.Entities
{
    public class Client : Entity<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string Reference { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual Guid? LiasonEmployeeId { get; set; }
        public virtual Agreement CurrentAgreement { get; set; }
 
        public static void Initialize(
            Guid clientId,
            Guid agreementId,
            DateTime agreementCommencement,
            DateTime agreementExpiry,
            int agreementValue, 
            IList<Service> agreementServices)
        {
            var client = new Client
                             {
                                 Id = clientId
                             };

            client.CurrentAgreement = Agreement.Initialize(
                agreementId,
                client,
                agreementCommencement,
                agreementExpiry,
                agreementValue,
                agreementServices);

            DomainEvents.Raise(new ClientInitializedEvent(client));
        }

        public virtual void Activate(
            string name,
            string reference,
            string address1,
            string address2,
            string address3,
            string phoneNumber,
            Guid? liasonEmployeeId)
        {
            Name = name;
            Reference = reference;
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            PhoneNumber = phoneNumber;
            LiasonEmployeeId = liasonEmployeeId;
            CurrentAgreement.Activate();
            DomainEvents.Raise(new ClientActivatedEvent(this));
        }

        public virtual void UnassignLiason()
        {
            LiasonEmployeeId = null;
            DomainEvents.Raise(new ClientLiasonUnassignedEvent(this));
        }
    }
}
