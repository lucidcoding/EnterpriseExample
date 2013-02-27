using System.Collections.Generic;
using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class LeadsUnassignedDomainEvent : DomainEvent<IList<Lead>>
    {
        public LeadsUnassignedDomainEvent(IList<Lead> leads)
            : base(leads)
        {
        }
    }
}
