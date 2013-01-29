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
            string clientName,
            string clientAddress1,
            string clientAddress2,
            string clientAddress3,
            string clientPhoneNumber,
            Guid dealId,
            DateTime agreementCommencement,
            DateTime agreementExpiry,
            IList<Service> agreementServices)
        {
            var client = new Client
                             {
                                 Id = clientId,
                                 Name = clientName,
                                 Address1 = clientAddress1,
                                 Address2 = clientAddress2,
                                 Address3 = clientAddress3,
                                 PhoneNumber = clientPhoneNumber
                             };

            client.CurrentAgreement = Agreement.Initialize(
                dealId,
                client,
                agreementCommencement,
                agreementExpiry,
                agreementServices);

            DomainEvents.Raise(new ClientInitializedDomainEvent(client));
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
            DomainEvents.Raise(new ClientActivatedDomainEvent(this));
        }

        public virtual void UnassignLiason()
        {
            LiasonEmployeeId = null;
            DomainEvents.Raise(new ClientLiasonUnassignedDomainEvent(this));
        }
    }
}
