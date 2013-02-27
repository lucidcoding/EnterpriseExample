using System;
using System.Collections.Generic;
using NServiceBus;

namespace Sales.Messages.Events
{
    public class LeadsUnassigned : IEvent
    {
        public List<Guid> UnassignedLeadsIds { get; set; } 
    }
}
