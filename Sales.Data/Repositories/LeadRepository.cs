using System;
using System.Collections.Generic;
using NHibernate.Criterion;
using Sales.Data.Common;
using Sales.Domain.Entities;
using Sales.Domain.RepositoryContracts;

namespace Sales.Data.Repositories
{
    public class LeadRepository : Repository<Lead, Guid>, ILeadRepository
    {
        public LeadRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }

        public IList<Lead> GetUnsigned()
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Lead>()
                .Add(Restrictions.Eq("SignedUp", false))
                .List<Lead>();
        }

        public IList<Lead> GetUnsignedByAssignedToConsultantId(Guid consultantId)
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Lead>()
                .Add(Restrictions.Eq("SignedUp", false))
                .Add(Restrictions.Eq("AssignedToConsultantId", consultantId))
                .List<Lead>();
        }
    }
}
