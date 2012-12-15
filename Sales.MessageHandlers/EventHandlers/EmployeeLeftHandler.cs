using HumanResources.Messages.Events;
using NServiceBus;
using Sales.Domain.Common;
using Sales.Domain.Events;
using Sales.Domain.RepositoryContracts;

namespace Sales.MessageHandlers.EventHandlers
{
    //Todo: do this too for client services.
    public class EmployeeLeftHandler : IHandleMessages<EmployeeLeft>
    {
        private readonly ILeadRepository _leadRepository;

        public EmployeeLeftHandler(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        public void Handle(EmployeeLeft @event)
        {
            //Unassign all leads assigned to the consultant who has left.
            //TODO: This send an email to the manager to warn that emails have been unassigned.

            DomainEvents.Register<LeadUnassignedEvent>(LeadUnassignedEventHandler);
            var leads = _leadRepository.GetUnsignedByAssignedToConsultantId(@event.Id);

            foreach (var lead in leads)
            {
                lead.Unassign();
            }
        }

        private void LeadUnassignedEventHandler(LeadUnassignedEvent @event)
        {
            _leadRepository.Save(@event.Source);
        }
    }
}
