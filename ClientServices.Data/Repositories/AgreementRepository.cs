using System;
using ClientServices.Data.Common;
using ClientServices.Domain.Entities;
using ClientServices.Domain.RepositoryContracts;

namespace ClientServices.Data.Repositories
{
    public class AgreementRepository : Repository<Agreement, Guid>, IAgreementRepository
    {
        public AgreementRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }
    }
}
