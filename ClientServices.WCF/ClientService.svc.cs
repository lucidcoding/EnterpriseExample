using System;
using System.Linq;
using System.Transactions;
using ClientServices.Data.Common;
using ClientServices.Domain.RepositoryContracts;
using ClientServices.WCF.DataTransferObjects;
using ClientServices.WCF.Mappers;
using StructureMap;

namespace ClientServices.WCF
{
    public class ClientService : IClientService
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly IClientRepository _clientRepository;

        public ClientService()
        {
            _sessionProvider = ObjectFactory.GetInstance<ISessionProvider>(); 
            _clientRepository = ObjectFactory.GetInstance<IClientRepository>();        
        }

        public ClientDto GetById(Guid id)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var clientDto = new ClientDtoMapper().Map(_clientRepository.GetById(id));
                    transactionScope.Complete();
                    return clientDto;
                }
            }
            finally
            {
                _sessionProvider.CloseCurrent();
            }
        }

        public ClientDto[] GetByIds(Guid[] ids)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var clientDtos = new ClientDtoMapper().Map(_clientRepository.GetByIds(ids.ToArray()).ToArray());
                    transactionScope.Complete();
                    return clientDtos;
                }
            }
            finally
            {
                _sessionProvider.CloseCurrent();
            }
        }
    }
}
