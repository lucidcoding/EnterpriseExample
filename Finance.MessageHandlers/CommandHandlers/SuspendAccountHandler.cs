using Finance.Domain.Common;
using Finance.Domain.Events;
using Finance.Domain.RepositoryContracts;
using Finance.Messages.Commands;
using Finance.Messages.Events;
using Finance.Messages.Replies;
using NServiceBus;

namespace Finance.MessageHandlers.CommandHandlers
{
    public class SuspendAccountHandler : IHandleMessages<SuspendAccount>
    {
        private readonly IBus _bus;
        private readonly IAccountRepository _accountRepository;

        public SuspendAccountHandler(
            IBus bus,
            IAccountRepository accountRepository)
        {
            _bus = bus;
            _accountRepository = accountRepository;
        }

        public void Handle(SuspendAccount message)
        {
            DomainEvents.Register<AccountSuspendedDomainEvent>(AccountSuspendedDomainEventHandler);
            var account = _accountRepository.GetById(message.Id);
            account.Suspend();
            _accountRepository.Flush();
            _bus.Return(ReturnCode.OK);
        }

        public void AccountSuspendedDomainEventHandler(AccountSuspendedDomainEvent @event)
        {
            _accountRepository.Save(@event.Source);
            _bus.Publish(new AccountSuspended { Id = @event.Source.Id.Value, AgreementId = @event.Source.AgreementId });
        }
    }
}
