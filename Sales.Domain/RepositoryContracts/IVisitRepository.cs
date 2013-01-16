using System;
using System.Collections.Generic;
using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.RepositoryContracts
{
    public interface IVisitRepository : IRepository<Visit, Guid>
    {
        IList<Visit> GetBookedByLeadId(Guid leadId);
        IList<Visit> GetCompletedByLeadId(Guid leadId);
    }
}
