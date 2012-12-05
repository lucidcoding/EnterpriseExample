using System.Collections.Generic;
using System.Transactions;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Domain.Entities;
using Sales.Domain.RepositoryContracts;

namespace Sales.Application.Implementations
{
    public class LeadService : ILeadService
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IBus _bus;

        public LeadService(
            ILeadRepository leadRepository,
            IBus bus)
        {
            _bus = bus;
            _leadRepository = leadRepository;
        }

        public IList<Lead> GetUnsigned()
        {
            using (var transactionScope = new TransactionScope())
            {
                var leads = _leadRepository.GetUnsigned();
                transactionScope.Complete();
                return leads;
            }
        }
    }
}
