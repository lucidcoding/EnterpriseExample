using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class VisitMadeEvent : DomainEvent<Visit>
    {
        public VisitMadeEvent(Visit visit)
            : base(visit)
        {
        }
    }
}
