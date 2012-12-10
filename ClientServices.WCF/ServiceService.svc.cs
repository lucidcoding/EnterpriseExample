using System.Linq;
using System.Transactions;
using ClientServices.Data.Common;
using ClientServices.Domain.RepositoryContracts;
using ClientServices.WCF.DataTransferObjects;
using ClientServices.WCF.Mappers;
using StructureMap;

namespace ClientServices.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceService" in code, svc and config file together.
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ISessionProvider _sessionProvider;

        public ServiceService()
        {
            //ObjectFactory.Container.Configure(x => x.AddRegistry<WcfRegistry>());
            _serviceRepository = ObjectFactory.GetInstance<IServiceRepository>();
            _sessionProvider = ObjectFactory.GetInstance<ISessionProvider>();
        }

        public ServiceDto[] GetAll()
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var employeeDtos = new ServiceDtoMapper().Map(_serviceRepository.GetAll().ToArray());
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
