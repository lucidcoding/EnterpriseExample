using System;
using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.RepositoryContracts
{
    public interface IDealRepository : IRepository<Deal, Guid>
    {
    }
}
