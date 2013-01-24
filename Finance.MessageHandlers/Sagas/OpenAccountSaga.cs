using ClientServices.Messages.Events;
using Finance.Domain.Common;
using Finance.Domain.Entities;
using Finance.Domain.Events;
using Finance.Domain.RepositoryContracts;
using Finance.MessageHandlers.SagaData;
using NServiceBus.Saga;
using Sales.Messages.Events;

namespace Finance.MessageHandlers.Sagas
{
    public class OpenAccountSaga :
        Saga<OpenAccountSagaData>,
        ISagaStartedBy<DealRegistered>,
        ISagaStartedBy<AgreementActivated> 
    {
        public IAccountRepository AccountRepository { get; set; }

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<DealRegistered>(
                saga => saga.DealId,
                message => message.Id);

            ConfigureMapping<AgreementActivated>(
                saga => saga.DealId,
                message => message.DealId);
        }

        public void Handle(DealRegistered message)
        {
            Data.DealRegisteredReceived = true;
            Data.DealId = message.Id;
            Data.Value = message.Value;
            CompleteIfPossible();
        }

        public void Handle(AgreementActivated message)
        {
            Data.AgreementActivatedReceived = true;
            Data.DealId = message.DealId;
            Data.AgreementId = message.Id;
            Data.Commencement = message.Commencement;
            Data.Expiry = message.Expiry;
            Data.ClientId = message.ClientId;
            CompleteIfPossible();
        }

        private void CompleteIfPossible()
        {
            if (Data.DealRegisteredReceived && Data.AgreementActivatedReceived)
            {
                DomainEvents.Register<AccountOpenedEvent>(AccountOpenedEventHandler);

                Account.Open(
                    Data.ClientId,
                    Data.AgreementId,
                    Data.Commencement,
                    Data.Expiry,
                    Data.Value);

                MarkAsComplete();
                AccountRepository.Flush();
            }
        }

        public void AccountOpenedEventHandler(AccountOpenedEvent @event)
        {
            AccountRepository.Save(@event.Source);
        }
    }
}
