using System;
using System.Collections.Generic;
using HumanResources.Domain.Entities;

namespace HumanResources.Application.Contracts
{
    public interface IEmployeeService
    {
        IList<Employee> GetCurrent();
        Employee GetById(Guid id);
        void MarkAsLeft(Guid id);
    }
}
