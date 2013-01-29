using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class LeadAddedDomainEvent : DomainEvent<Lead>
    {
        public LeadAddedDomainEvent(Lead lead)
            : base(lead)
        {
        }
    }
}
