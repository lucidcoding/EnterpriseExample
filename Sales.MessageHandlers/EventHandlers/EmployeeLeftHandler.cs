using HumanResources.Messages.Events;
using NServiceBus;
using Sales.Domain.Common;
using Sales.Domain.Events;
using Sales.Domain.RepositoryContracts;

namespace Sales.MessageHandlers.EventHandlers
{
    public class EmployeeLeftHandler : IHandleMessages<EmployeeLeft>
    {
        private readonly ILeadRepository _leadRepository;

        public EmployeeLeftHandler(ILeadRepository leadRepository)
        {
            _leadRepository = leadRepository;
        }

        public void Handle(EmployeeLeft @event)
        {
            DomainEvents.Register<LeadUnassignedDomainEvent>(LeadUnassignedDomainEventHandler);
            var leads = _leadRepository.GetUnsignedByAssignedToConsultantId(@event.Id);

            foreach (var lead in leads)
            {
                lead.Unassign();
            }
        }

        private void LeadUnassignedDomainEventHandler(LeadUnassignedDomainEvent @event)
        {
            _leadRepository.Save(@event.Source);
        }
    }
}
