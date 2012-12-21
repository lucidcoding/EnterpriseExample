using System;
using System.Collections.Generic;
using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.RepositoryContracts
{
    public interface IAgreementRepository : IRepository<Agreement, Guid>
    {
        IList<Agreement> GetByClientId(Guid clientId);
    }
}
