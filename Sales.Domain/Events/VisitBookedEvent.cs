using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class VisitBookedEvent : DomainEvent<Visit>
    {
        public VisitBookedEvent(Visit visit)
            : base(visit)
        {
        }
    }
}
