using System;
using Finance.Data.Common;
using Finance.Domain.Entities;
using Finance.Domain.RepositoryContracts;
using NHibernate.Criterion;

namespace Finance.Data.Repositories
{
    public class AccountRepository : Repository<Account, Guid>, IAccountRepository
    {
        public AccountRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }

        public Account GetByAgreementId(Guid agreementId)
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Account>()
                .Add(Restrictions.Eq("AgreementId", agreementId))
                .UniqueResult<Account>();
        }
    }
}
