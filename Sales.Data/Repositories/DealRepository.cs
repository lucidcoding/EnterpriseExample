using System;
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
    }
}
