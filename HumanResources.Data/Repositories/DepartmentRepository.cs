using System;
using HumanResources.Data.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.RepositoryContracts;

namespace HumanResources.Data.Repositories
{
    public class DepartmentRepository : Repository<Department, Guid>, IDepartmentRepository
    {
        public DepartmentRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }
    }
}
