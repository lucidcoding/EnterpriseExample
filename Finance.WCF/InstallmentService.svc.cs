using System;
using System.Transactions;
using Finance.Data.Common;
using Finance.Domain.RepositoryContracts;
using Finance.WCF.DataTransferObjects;
using Finance.WCF.Mappers;
using StructureMap;

namespace Finance.WCF
{
    public class InstallmentService : IInstallmentService
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly IInstallmentRepository _installmentRepository;

        public InstallmentService()
        {
            _sessionProvider = ObjectFactory.GetInstance<ISessionProvider>();
            _installmentRepository = ObjectFactory.GetInstance<IInstallmentRepository>();
        }

        public InstallmentDto GetById(Guid id)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var installmentDto = new InstallmentDtoMapper().MapWithAccount(_installmentRepository.GetById(id));
                    transactionScope.Complete();
                    return installmentDto;
                }
            }
            finally
            {
                _sessionProvider.CloseCurrent();
            }
        }
    }
}
