using System;
using ClientServices.Data.Common;
using ClientServices.Domain.Entities;
using ClientServices.Domain.RepositoryContracts;

namespace ClientServices.Data.Repositories
{
    public class ServiceRepository : Repository<Service, Guid>, IServiceRepository
    {
        public ServiceRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }
    }
}
