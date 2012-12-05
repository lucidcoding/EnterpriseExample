using System;
using NHibernate;
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
    }
}
