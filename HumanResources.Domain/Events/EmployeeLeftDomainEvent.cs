using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Events
{
    public class EmployeeLeftDomainEvent : DomainEvent<Employee>
    {
        public EmployeeLeftDomainEvent(Employee employee)
            : base(employee)
        {
        }
    }
}
