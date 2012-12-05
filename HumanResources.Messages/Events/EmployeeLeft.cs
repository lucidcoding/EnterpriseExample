using System;
using NServiceBus;

namespace HumanResources.Messages.Events
{
    public class EmployeeLeft : IEvent
    {
        public Guid Id { get; set; }
        public virtual DateTime? Left { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
