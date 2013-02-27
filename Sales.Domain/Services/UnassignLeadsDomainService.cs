using System.Collections.Generic;
using Sales.Domain.Common;
using Sales.Domain.Entities;
using Sales.Domain.Events;

namespace Sales.Domain.Services
{
    public static class UnassignLeadsDomainService
    {
        public static void Unassign(IList<Lead> leads)
        {
            foreach (var lead in leads)
            {
                lead.Unassign();
            }

            DomainEvents.Raise(new LeadsUnassignedDomainEvent(leads));
        }
    }
}
