using ClientServices.Domain.Common;
using ClientServices.Domain.Events;
using ClientServices.Domain.RepositoryContracts;
using HumanResources.Messages.Events;
using NServiceBus;

namespace ClientServices.MessageHandlers.EventHandlers
{
    //Todo: do this too for client services.
    public class EmployeeLeftHandler : IHandleMessages<EmployeeLeft>
    {
        private readonly IClientRepository _clientRepository;

        public EmployeeLeftHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public void Handle(EmployeeLeft @event)
        {
            //TODO: This send an email to the manager to warn that clients have been unassigned.

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
