using System.Collections.Generic;
using Sales.Domain.Entities;

namespace Sales.Application.Contracts
{
    public interface ILeadService
    {
        IList<Lead> GetUnsigned();
    }
}
