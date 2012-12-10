using System;
using System.Linq;
using System.Transactions;
using HumanResources.Data.Common;
using HumanResources.Domain.RepositoryContracts;
using HumanResources.WCF.Core;
using HumanResources.WCF.DataTransferObjects;
using HumanResources.WCF.Mappers;
using StructureMap;

namespace HumanResources.WCF
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISessionProvider _sessionProvider;

        //todo: there is a better way to do IOC with WCF, but haven't got it working.
        public EmployeeService()
        {
            ObjectFactory.Container.Configure(x => x.AddRegistry<WcfRegistry>());
            _employeeRepository = ObjectFactory.GetInstance<IEmployeeRepository>();
            _sessionProvider = ObjectFactory.GetInstance<ISessionProvider>();
        }

        public EmployeeDto[] GetAll()
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var employeeDtos = new EmployeeDtoMapper().Map(_employeeRepository.GetCurrent().ToArray());
                    transactionScope.Complete();
                    return employeeDtos;
                }
            }
            finally
            {
                _sessionProvider.CloseCurrent();
            }
        }

        public EmployeeDto[] GetByIds(Guid[] ids)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var employeeDtos = new EmployeeDtoMapper().Map(_employeeRepository.GetByIds(ids.ToList()).ToArray());
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
                    var employeeDtos = new EmployeeDtoMapper().Map(_employeeRepository.GetById(id));
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
