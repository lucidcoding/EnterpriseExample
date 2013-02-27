using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HumanResources.Data.Common;
using HumanResources.WCF.Core;
using HumanResources.WCF.DataTransferObjects;
using System.Transactions;
using HumanResources.WCF.Mappers;
using StructureMap;
using HumanResources.Domain.RepositoryContracts;

namespace HumanResources.WCF
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISessionProvider _sessionProvider;

        public DepartmentService()
        {
            ObjectFactory.Container.Configure(x => x.AddRegistry<WcfRegistry>());
            _departmentRepository = ObjectFactory.GetInstance<IDepartmentRepository>();
            _sessionProvider = ObjectFactory.GetInstance<ISessionProvider>();
        }

        public DepartmentDto GetByIdWithManager(Guid id)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var depatrtmentDto = new DepartmentDtoMapper().MapWithManager(_departmentRepository.GetById(id));
                    transactionScope.Complete();
                    return depatrtmentDto;
                }
            }
            finally
            {
                _sessionProvider.CloseCurrent();
            }
        }
    }
}
