using System;
using Finance.Domain.Common;
using Finance.Domain.Entities;

namespace Finance.Domain.RepositoryContracts
{
    public interface IAccountRepository : IRepository<Account, Guid>
    {
    }
}
