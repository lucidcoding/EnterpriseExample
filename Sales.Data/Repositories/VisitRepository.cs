using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Sales.Data.Common;
using Sales.Domain.Entities;
using Sales.Domain.RepositoryContracts;

namespace Sales.Data.Repositories
{
    public class  VisitRepository : Repository<Visit, Guid>, IVisitRepository
    {
        public VisitRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }

        public IList<Visit> GetByLeadId(Guid leadId)
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Visit>()
                .Add(Restrictions.Eq("Lead.Id", leadId))
                .List<Visit>();
        }
    }
}
