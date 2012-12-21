using System;
using System.Collections.Generic;
using ClientServices.Data.Common;
using ClientServices.Domain.Entities;
using ClientServices.Domain.RepositoryContracts;
using NHibernate.Criterion;

namespace ClientServices.Data.Repositories
{
    public class AgreementRepository : Repository<Agreement, Guid>, IAgreementRepository
    {
        public AgreementRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }

        public IList<Agreement> GetByClientId(Guid clientId)
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Agreement>()
                .Add(Restrictions.Eq("Client.Id", clientId))
                .List<Agreement>();
        }
    }
}
