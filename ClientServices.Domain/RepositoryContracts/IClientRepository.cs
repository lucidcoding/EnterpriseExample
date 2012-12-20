using System;
using System.Collections.Generic;
using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.RepositoryContracts
{
    public interface IClientRepository : IRepository<Client, Guid>
    {
        IList<Client> GetInitialized();
        IList<Client> GetActive();
        IList<Client> GetByLiasonEmployeeId(Guid liasonEmployeeId);
    }
}
