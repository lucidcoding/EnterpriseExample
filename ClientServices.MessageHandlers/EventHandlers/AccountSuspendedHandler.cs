﻿using ClientServices.Domain.Common;
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
            DomainEvents.Register<AgreementSuspendedEvent>(AgreementSuspendedEventHandler);
            var agreement = _agreementRepository.GetById(message.Id);
            agreement.Suspend();
            _agreementRepository.Flush();
        }

        public void AgreementSuspendedEventHandler(AgreementSuspendedEvent @event)
        {
            _agreementRepository.Update(@event.Source);
        }
    }
}
