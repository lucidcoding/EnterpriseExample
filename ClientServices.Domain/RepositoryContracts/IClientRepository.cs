using System;
using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.RepositoryContracts
{
    public interface IClientRepository : IRepository<Client, Guid>
    {
    }
}
