using System;
using System.Collections.Generic;
using Finance.Domain.Common;
using Finance.Domain.Entities;

namespace Finance.Domain.RepositoryContracts
{
    public interface IInstallmentRepository : IRepository<Installment, Guid>
    {
        IList<Installment> GetByAccountId(Guid accountId);
    }
}
