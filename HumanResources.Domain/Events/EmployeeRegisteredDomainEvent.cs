using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Events
{
    public class EmployeeRegisteredDomainEvent : DomainEvent<Employee>
    {
        public EmployeeRegisteredDomainEvent(Employee employee)
            : base(employee)
        {
        }
    }
}
