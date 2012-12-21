using System;
using System.Collections.Generic;
using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.RepositoryContracts
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
        IList<Employee> GetCurrent();
        IList<Employee> GetCurrentByDepartmentId(Guid departmentId);
    }
}
