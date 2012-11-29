﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Transactions;
using HumanResources.Data.Common;
using HumanResources.WCF.DataTransferObjects;
using HumanResources.WCF.Mappers;
using StructureMap;

namespace HumanResources.WCF
{
    public class EmployeeService : IEmployeeService
    {
        private readonly Application.Contracts.IEmployeeService _employeeService;
        private readonly ISessionProvider _sessionProvider;

        //todo: there is a better way to do IOC with WCF, but haven't got it working.
        public EmployeeService()
        {
            _employeeService = ObjectFactory.GetInstance<Application.Contracts.IEmployeeService>();
            _sessionProvider = ObjectFactory.GetInstance<ISessionProvider>();
        }

        public EmployeeDto[] GetAll()
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var employeeDtos = new EmployeeDtoMapper().Map(_employeeService.GetAll().ToArray());
                    transactionScope.Complete();
                    return employeeDtos;
                }
            }
            finally
            {
                _sessionProvider.CloseCurrent();
            }
        }

        public EmployeeDto GetById(Guid id)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var employeeDtos = new EmployeeDtoMapper().Map(_employeeService.GetById(id));
                    transactionScope.Complete();
                    return employeeDtos;
                }
            }
            finally
            {
                _sessionProvider.CloseCurrent();
            }
        }
    }
}
