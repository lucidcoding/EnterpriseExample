using System;
using HumanResources.Data.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.RepositoryContracts;

namespace HumanResources.Data.Repositories
{
    public class EmployeeRepository : Repository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }
    }
}
