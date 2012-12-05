using System;
using System.Collections.Generic;
using HumanResources.Data.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.RepositoryContracts;
using NHibernate.Criterion;

namespace HumanResources.Data.Repositories
{
    public class EmployeeRepository : Repository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }

        public IList<Employee> GetCurrent()
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Employee>()
                .Add(Restrictions.IsNull("Left"))
                .List<Employee>();
        }
    }
}
