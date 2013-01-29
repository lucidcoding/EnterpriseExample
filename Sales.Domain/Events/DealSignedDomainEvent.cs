using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class DealSignedDomainEvent : DomainEvent<Deal>
    {
        public DealSignedDomainEvent(Deal deal)
            : base(deal)
        {
        }
    }
}
