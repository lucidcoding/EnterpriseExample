using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using Sales.Data.Common;
using Sales.Domain.Entities;
using Sales.Domain.RepositoryContracts;

namespace Sales.Data.Repositories
{
    public class DealRepository : Repository<Deal, Guid>, IDealRepository
    {
        public DealRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }

        public IList<Deal> GetByLeadId(Guid leadId)
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Deal>()
                .Add(Restrictions.Eq("Lead.Id", leadId))
                .List<Deal>();
        }
    }
}
