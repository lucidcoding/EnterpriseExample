using System;
using Sales.Domain.Common;
using Sales.Domain.Events;

namespace Sales.Domain.Entities
{
    public class Deal : Entity<Guid>
    {
        public virtual Lead Lead { get; set; }
        public virtual Guid MadeByConsultantId { get; set; }
        public virtual int Value { get; set; }
        public virtual int Commission { get; set; }

        public static void Register(Guid id, Lead lead, int value)
        {
            var deal = new Deal
                           {
                               Id = id,
                               Lead = lead,
                               MadeByConsultantId = lead.AssignedToConsultantId.Value,
                               Value = value,
                               Commission = value/10
                           };

            lead.MarkAsSignedUp();
            DomainEvents.Raise(new DealSignedDomainEvent(deal));
        }
    }
}
