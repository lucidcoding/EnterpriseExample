using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class VisitCompletedEvent : DomainEvent<Visit>
    {
        public VisitCompletedEvent(Visit visit)
            : base(visit)
        {
        }
    }
}
