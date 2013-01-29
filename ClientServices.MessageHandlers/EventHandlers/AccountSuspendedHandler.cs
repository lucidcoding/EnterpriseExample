using ClientServices.Domain.Common;
using ClientServices.Domain.Events;
using ClientServices.Domain.RepositoryContracts;
using Finance.Messages.Events;
using NServiceBus;

namespace ClientServices.MessageHandlers.EventHandlers
{
    public class AccountSuspendedHandler: IHandleMessages<AccountSuspended>
    {
        private readonly IAgreementRepository _agreementRepository;

        public AccountSuspendedHandler(IAgreementRepository agreementRepository)
        {
            _agreementRepository = agreementRepository;
        }

        public void Handle(AccountSuspended message)
        {
            DomainEvents.Register<AgreementSuspendedDomainEvent>(AgreementSuspendedDomainEventHandler);
            var agreement = _agreementRepository.GetById(message.AgreementId);
            agreement.Suspend();
            _agreementRepository.Flush();
        }

        public void AgreementSuspendedDomainEventHandler(AgreementSuspendedDomainEvent @event)
        {
            _agreementRepository.Update(@event.Source);
        }
    }
}
