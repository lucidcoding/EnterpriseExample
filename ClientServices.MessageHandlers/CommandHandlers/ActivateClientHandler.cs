using ClientServices.Domain.Common;
using ClientServices.Domain.Events;
using ClientServices.Domain.RepositoryContracts;
using ClientServices.Messages.Commands;
using ClientServices.Messages.Events;
using ClientServices.Messages.Replies;
using NServiceBus;

namespace ClientServices.MessageHandlers.CommandHandlers
{
    public class ActivateClientHandler : IHandleMessages<ActivateClient>
    {
        private readonly IBus _bus;
        private readonly IClientRepository _clientRepository;

        public ActivateClientHandler(
            IBus bus,
            IClientRepository clientRepository)
        {
            _bus = bus;
            _clientRepository = clientRepository;
        }

        public void Handle(ActivateClient message)
        {
            DomainEvents.Register<ClientActivatedEvent>(ClientActivatedEventHandler);
            DomainEvents.Register<AgreementActivatedEvent>(AgreementActivatedEventHandler);
            var clientDetails = _clientRepository.GetById(message.Id);

            clientDetails.Activate(
                message.Name,
                message.Reference,
                message.Address1,
                message.Address2,
                message.Address3,
                message.PhoneNumber,
                message.LiasonEmployeeId);

            _clientRepository.Flush();
            _bus.Return(ReturnCode.OK);
        }

        public void ClientActivatedEventHandler(ClientActivatedEvent @event)
        {
            _clientRepository.Save(@event.Source);
        }

        public void AgreementActivatedEventHandler(AgreementActivatedEvent @event)
        {
            var agreementActivated = new AgreementActivated
                                         {
                                             Id = @event.Source.Id.Value,
                                             DealId = @event.Source.DealId,
                                             ClientId = @event.Source.Client.Id.Value,
                                             Commencement = @event.Source.Commencement,
                                             Expiry = @event.Source.Expiry
                                         };

            _bus.Publish(agreementActivated);
        }
    }
}
