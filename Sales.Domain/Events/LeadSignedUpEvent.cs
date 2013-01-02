using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class LeadSignedUpEventEvent : DomainEvent<Lead>
    {
        public LeadSignedUpEventEvent(Lead lead)
            : base(lead)
        {
        }
    }
}
