using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;
using ClientServices.Domain.Events;
using ClientServices.Domain.RepositoryContracts;
using ClientServices.Messages.Commands;
using ClientServices.Messages.Replies;
using NServiceBus;

namespace ClientServices.MessageHandlers.CommandHandlers
{
    public class InitializeClientHandler : IHandleMessages<InitializeClient>
    {
        private readonly IBus _bus;
        private readonly IClientRepository _clientRepository;
        private readonly IServiceRepository _serviceRepository;

        public InitializeClientHandler(
            IBus bus,
            IClientRepository clientRepository,
            IServiceRepository serviceRepository)
        {
            _bus = bus;
            _clientRepository = clientRepository;
            _serviceRepository = serviceRepository;
        }

        public void Handle(InitializeClient message)
        {
            DomainEvents.Register<ClientInitializedEvent>(ClientInitializedEventHandler);
            var services = _serviceRepository.GetByIds(message.AgreementServiceIds);

            Client.Initialize(
                message.ClientId,
                message.AgreementId,
                message.AgreementCommencement,
                message.AgreementExpiry,
                message.AgreementValue,
                services);

            _clientRepository.Flush();
            _bus.Return(ReturnCode.OK);
        }

        public void ClientInitializedEventHandler(ClientInitializedEvent @event)
        {
            _clientRepository.Save(@event.Source);
        }
    }
}
