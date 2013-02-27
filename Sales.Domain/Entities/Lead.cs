using System;
using Sales.Domain.Common;
using Sales.Domain.Events;

namespace Sales.Domain.Entities
{
    public class Lead : Entity<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual Guid? AssignedToConsultantId { get; set; }
        public virtual bool SignedUp { get; set; }

        public static void Add(Guid id, string name, string address1, string address2, string address3, string phoneNumber, Guid assignedToConsultantId)
        {
            var lead = new Lead
                           {
                               Id = id,
                               Name = name,
                               Address1 = address1,
                               Address2 = address2,
                               Address3 = address3,
                               AssignedToConsultantId = assignedToConsultantId,
                               SignedUp = false
                           };

            DomainEvents.Raise(new LeadAddedDomainEvent(lead));
        }

        public virtual void MarkAsSignedUp()
        {
            SignedUp = true;
            DomainEvents.Raise(new LeadSignedUpEventEvent(this));
        }

        public virtual void Unassign()
        {
            AssignedToConsultantId = null;
        }
    }
}
