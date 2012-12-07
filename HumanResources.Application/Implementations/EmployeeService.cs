﻿using System;
using System.Collections.Generic;
using System.Transactions;
using HumanResources.Application.Contracts;
using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.RepositoryContracts;
using HumanResources.Messages.Events;
using NServiceBus;

namespace HumanResources.Application.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBus _bus;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IBus bus)
        {
            _bus = bus;
            _employeeRepository = employeeRepository;
        }

        public IList<Employee> GetCurrent()
        {
            using (var transactionScope = new TransactionScope())
            {
                var employees = _employeeRepository.GetCurrent();
                transactionScope.Complete();
                return employees;
            }
        }

        public IList<Employee> GetByIds(IList<Guid> ids)
        {
            using (var transactionScope = new TransactionScope())
            {
                var employees = _employeeRepository.GetByIds(ids);
                transactionScope.Complete();
                return employees;
            }
        }

        public Employee GetById(Guid id)
        {
            using (var transactionScope = new TransactionScope())
            {
                var employee = _employeeRepository.GetById(id);
                transactionScope.Complete();
                return employee;
            }
        }
    }
}
