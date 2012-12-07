using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class LeadUnassignedEvent : DomainEvent<Lead>
    {
        public LeadUnassignedEvent(Lead lead)
            : base(lead)
        {
        }
    }
}
