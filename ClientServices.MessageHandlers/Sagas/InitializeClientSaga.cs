using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;
using ClientServices.Domain.Events;
using ClientServices.Domain.RepositoryContracts;
using ClientServices.MessageHandlers.SagaData;
using ClientServices.Messages.Commands;
using NServiceBus.Saga;
using Sales.Messages.Events;

namespace ClientServices.MessageHandlers.Sagas
{
    public class InitializeClientSaga : 
        Saga<InitializeClientSagaData>,
        ISagaStartedBy<LeadSignedUp>,
        ISagaStartedBy<InitializeClient>
    {
        public IClientRepository ClientRepository { get; set; }
        public IServiceRepository ServiceRepository { get; set; }

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<LeadSignedUp>(
                saga => saga.CorrelationId,
                message => message.CorrelationId);

            ConfigureMapping<InitializeClient>(
                saga => saga.CorrelationId,
                message => message.CorrelationId);
        }

        public void Handle(LeadSignedUp message)
        {
            Data.CorrelationId = message.CorrelationId;
            Data.LeadSignedUpReceived = true;
            Data.ClientId = message.LeadId;
            Data.ClientName = message.Name;
            Data.ClientAddress1 = message.Address1;
            Data.ClientAddress2 = message.Address2;
            Data.ClientAddress3 = message.Address3;
            Data.ClientPhoneNumber = message.PhoneNumber;
            CompleteIfPossible();
        }

        public void Handle(InitializeClient message)
        {
            Data.CorrelationId = message.CorrelationId;
            Data.InitializeClientReceived = true;
            Data.ClientId = message.ClientId;
            Data.AgreementId = message.AgreementId;
            Data.AgreementCommencement = message.AgreementCommencement;
            Data.AgreementExpiry = message.AgreementExpiry;
            Data.AgreementValue = message.AgreementValue;
            Data.AgreementServiceIds = message.AgreementServiceIds;
            CompleteIfPossible();
        }

        private void CompleteIfPossible()
        {
            if(Data.LeadSignedUpReceived && Data.InitializeClientReceived)
            {
                DomainEvents.Register<ClientInitializedEvent>(ClientInitializedEventHandler);
                var services = ServiceRepository.GetByIds(Data.AgreementServiceIds);

                Client.Initialize(
                    Data.ClientId,
                    Data.ClientName,
                    Data.ClientAddress1,
                    Data.ClientAddress2,
                    Data.ClientAddress3,
                    Data.ClientPhoneNumber,
                    Data.AgreementId,
                    Data.AgreementCommencement,
                    Data.AgreementExpiry,
                    Data.AgreementValue,
                    services);

                MarkAsComplete();
                ClientRepository.Flush();
            }
        }

        private void ClientInitializedEventHandler(ClientInitializedEvent @event)
        {
            ClientRepository.Save(@event.Source);
        }
    }
}
