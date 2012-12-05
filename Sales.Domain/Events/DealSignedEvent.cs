using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class DealSignedEvent : DomainEvent<Deal>
    {
        public DealSignedEvent(Deal deal)
            : base(deal)
        {
        }
    }
}
