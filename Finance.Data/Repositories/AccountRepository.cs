using System;
using Finance.Data.Common;
using Finance.Domain.Entities;
using Finance.Domain.RepositoryContracts;

namespace Finance.Data.Repositories
{
    public class AccountRepository : Repository<Account, Guid>, IAccountRepository
    {
        public AccountRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }
    }
}
