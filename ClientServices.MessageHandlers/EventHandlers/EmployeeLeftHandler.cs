using ClientServices.Domain.Common;
using ClientServices.Domain.Events;
using ClientServices.Domain.RepositoryContracts;
using HumanResources.Messages.Events;
using NServiceBus;

namespace ClientServices.MessageHandlers.EventHandlers
{
    public class EmployeeLeftHandler : IHandleMessages<EmployeeLeft>
    {
        private readonly IClientRepository _clientRepository;

        public EmployeeLeftHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public void Handle(EmployeeLeft @event)
        {
            DomainEvents.Register<ClientLiasonUnassignedEvent>(ClientLiasonUnassignedEventHandler);
            var clients = _clientRepository.GetByLiasonEmployeeId(@event.Id);

            foreach (var client in clients)
            {
                client.UnassignLiason();
            }
        }

        private void ClientLiasonUnassignedEventHandler(ClientLiasonUnassignedEvent @event)
        {
            _clientRepository.Save(@event.Source);
        }
    }
}
