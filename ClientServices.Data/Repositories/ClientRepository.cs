using System;
using System.Collections.Generic;
using ClientServices.Data.Common;
using ClientServices.Domain.Entities;
using ClientServices.Domain.Enumerations;
using ClientServices.Domain.RepositoryContracts;
using NHibernate.Criterion;

namespace ClientServices.Data.Repositories
{
    public class ClientRepository : Repository<Client, Guid>, IClientRepository
    {
        public ClientRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }

        public IList<Client> GetInitialized()
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Client>()
                .CreateAlias("CurrentAgreement", "currentAgreement")
                .Add(Restrictions.Eq("currentAgreement.Status", AgreementStatus.Initialized))
                .List<Client>();
        }

        public IList<Client> GetActive()
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Client>()
                .CreateAlias("CurrentAgreement", "currentAgreement")
                .Add(Restrictions.Eq("currentAgreement.Status", AgreementStatus.Active))
                .List<Client>();
        }

        public IList<Client> GetByLiasonEmployeeId(Guid liasonEmployeeId)
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Client>()
                .Add(Restrictions.Eq("LiasonEmployeeId", liasonEmployeeId))
                .List<Client>();
        }
    }
}
