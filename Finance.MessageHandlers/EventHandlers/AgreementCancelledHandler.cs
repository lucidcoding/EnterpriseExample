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
            DomainEvents.Register<AccountClosedEvent>(AccountClosedEventHandler);
            var account = _accountRepository.GetByAgreementId(message.Id);
            account.Close();
            _accountRepository.Flush();
        }

        public void AccountClosedEventHandler(AccountClosedEvent @event)
        {
            _accountRepository.Update(@event.Source);
        }
    }
}
