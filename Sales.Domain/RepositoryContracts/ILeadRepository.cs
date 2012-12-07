using System;
using System.Collections.Generic;
using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.RepositoryContracts
{
    public interface ILeadRepository : IRepository<Lead, Guid>
    {
        IList<Lead> GetUnsigned();
        IList<Lead> GetUnsignedByAssignedToConsultantId(Guid consultantId);
    }
}
