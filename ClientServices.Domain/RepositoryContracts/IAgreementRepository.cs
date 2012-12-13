using System;
using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.RepositoryContracts
{
    public interface IAgreementRepository : IRepository<Agreement, Guid>
    {
    }
}
