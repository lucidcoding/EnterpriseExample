using System;
using System.Linq;
using System.Transactions;
using Sales.Data.Common;
using Sales.Domain.RepositoryContracts;
using Sales.WCF.Core;
using Sales.WCF.DataTransferObjects;
using Sales.WCF.Mappers;
using StructureMap;

namespace Sales.WCF
{
    public class LeadService : ILeadService
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly ILeadRepository _leadRepository;

        public LeadService()
        {
            ObjectFactory.Container.Configure(x => x.AddRegistry<WcfRegistry>());
            _sessionProvider = ObjectFactory.GetInstance<ISessionProvider>();
            _leadRepository = ObjectFactory.GetInstance<ILeadRepository>();
        }

        public LeadDto[] GetByIds(Guid[] ids)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var employeeDtos = new LeadDtoMapper().Map(_leadRepository.GetByIds(ids.ToList()).ToArray());
                    transactionScope.Complete();
                    return employeeDtos;
                }
            }
            finally
            {
                _sessionProvider.CloseCurrent();
            }
        }

        public LeadDto GetById(Guid id)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var employeeDtos = new LeadDtoMapper().Map(_leadRepository.GetById(id));
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
