using ClientServices.Messages.Events;
using Finance.Domain.Common;
using Finance.Domain.Events;
using Finance.Domain.RepositoryContracts;
using NServiceBus;

namespace Finance.MessageHandlers.EventHandlers
{
    public class AgreementCancelledHandler : IHandleMessages<AgreementCancelled>
    {
        private readonly IAccountRepository _accountRepository;

        public AgreementCancelledHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Handle(AgreementCancelled message)
        {
            DomainEvents.Register<AccountClosedDomainEvent>(AccountClosedDomainEventHandler);
            var account = _accountRepository.GetByAgreementId(message.Id);
            account.Close();
            _accountRepository.Flush();
        }

        public void AccountClosedDomainEventHandler(AccountClosedDomainEvent @event)
        {
            _accountRepository.Update(@event.Source);
        }
    }
}
