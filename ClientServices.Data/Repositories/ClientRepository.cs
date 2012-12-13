using System;
using ClientServices.Data.Common;
using ClientServices.Domain.Entities;
using ClientServices.Domain.RepositoryContracts;

namespace ClientServices.Data.Repositories
{
    public class ClientRepository : Repository<Client, Guid>, IClientRepository
    {
        public ClientRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }
    }
}
