using System.Linq;
using HumanResources.Messages.Events;
using NServiceBus;
using Sales.Domain.Common;
using Sales.Domain.Events;
using Sales.Domain.RepositoryContracts;
using Sales.Domain.Services;
using Sales.Messages.Events;

namespace Sales.MessageHandlers.EventHandlers
{
    public class EmployeeLeftHandler : IHandleMessages<EmployeeLeft>
    {
        private readonly IBus _bus;
        private readonly ILeadRepository _leadRepository;

        public EmployeeLeftHandler(
            IBus bus,
            ILeadRepository leadRepository)
        {
            _bus = bus;
            _leadRepository = leadRepository;
        }

        public void Handle(EmployeeLeft @event)
        {
            DomainEvents.Register<LeadsUnassignedDomainEvent>(LeadUnassignedDomainEventHandler);
            var leads = _leadRepository.GetUnsignedByAssignedToConsultantId(@event.Id);
            UnassignLeadsDomainService.Unassign(leads);
            _leadRepository.Flush();
        }

        private void LeadUnassignedDomainEventHandler(LeadsUnassignedDomainEvent @event)
        {
            foreach (var lead in @event.Source)
            {
                _leadRepository.Save(lead);
            }

            _bus.Publish(new LeadsUnassigned
                             {
                                 UnassignedLeadsIds = @event.Source.Select(lead => lead.Id.Value).ToList()
                             });
        }
    }
}
