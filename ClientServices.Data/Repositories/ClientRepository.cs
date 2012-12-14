﻿using System;
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
                .Add(Restrictions.Eq("CurrentAgreement.Status", AgreementStatus.Initialized))
                .List<Client>();
        }

        public IList<Client> GetActive()
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Client>()
                .Add(Restrictions.Eq("CurrentAgreement.Status", AgreementStatus.Active))
                .List<Client>();
        }
    }
}
