using ClientServices.Messages.Events;
using Finance.Domain.Common;
using Finance.Domain.Entities;
using Finance.Domain.Events;
using Finance.Domain.RepositoryContracts;
using NServiceBus;

namespace Finance.MessageHandlers.EventHandlers
{
    public class AgreementActivatedHandler : IHandleMessages<AgreementActivated>
    {
        private readonly IAccountRepository _accountRepository;

        public AgreementActivatedHandler(
            IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Handle(AgreementActivated message)
        {
            DomainEvents.Register<AccountOpenedEvent>(AccountOpenedEventHandler);

            Account.Open(
                message.ClientId,
                message.Id,
                message.Commencement,
                message.Expiry,
                message.Value);

            _accountRepository.Flush();
        }

        public void AccountOpenedEventHandler(AccountOpenedEvent @event)
        {
            _accountRepository.Save(@event.Source);
        }
    }
}
