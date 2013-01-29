using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class VisitBookedDomainEvent : DomainEvent<Visit>
    {
        public VisitBookedDomainEvent(Visit visit)
            : base(visit)
        {
        }
    }
}
