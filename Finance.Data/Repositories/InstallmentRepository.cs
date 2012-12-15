using System;
using System.Collections.Generic;
using Finance.Data.Common;
using Finance.Domain.Entities;
using Finance.Domain.RepositoryContracts;
using NHibernate.Criterion;

namespace Finance.Data.Repositories
{
    public class InstallmentRepository : Repository<Installment, Guid>, IInstallmentRepository
    {
        public InstallmentRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }

        public IList<Installment> GetByAccountId(Guid accountId)
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Installment>()
                .Add(Restrictions.Eq("Account.Id", accountId))
                .List<Installment>();
        }
    }
}
