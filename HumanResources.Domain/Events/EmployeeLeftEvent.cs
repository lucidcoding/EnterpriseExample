using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Events
{
    public class EmployeeLeftEvent : DomainEvent<Employee>
    {
        public EmployeeLeftEvent(Employee employee)
            : base(employee)
        {
        }
    }
}
