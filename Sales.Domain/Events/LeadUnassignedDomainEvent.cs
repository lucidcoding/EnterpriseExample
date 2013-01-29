using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class LeadUnassignedDomainEvent : DomainEvent<Lead>
    {
        public LeadUnassignedDomainEvent(Lead lead)
            : base(lead)
        {
        }
    }
}
