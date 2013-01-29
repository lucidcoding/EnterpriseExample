using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class VisitCompletedDomainEvent : DomainEvent<Visit>
    {
        public VisitCompletedDomainEvent(Visit visit)
            : base(visit)
        {
        }
    }
}
