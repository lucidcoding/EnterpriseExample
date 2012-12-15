using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientServices.Messages.Events;
using Finance.Domain.Common;
using Finance.Domain.Events;
using Finance.Domain.RepositoryContracts;
using NServiceBus;

namespace Finance.MessageHandlers.EventHandlers
{
    public class AgreementCancelledHandler : IHandleMessages<AgreementCancelled>
    {
        private readonly IBus _bus;
        private readonly IAccountRepository _accountRepository;

        public AgreementCancelledHandler(
            IBus bus,
            IAccountRepository accountRepository)
        {
            _bus = bus;
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
