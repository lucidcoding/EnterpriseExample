using NServiceBus;
using Sales.Domain.Common;
using Sales.Domain.Entities;
using Sales.Domain.Events;
using Sales.Domain.RepositoryContracts;
using Sales.Messages.Commands;

namespace Sales.MessageHandlers.CommandHandlers
{
    public class LogVisitHandler : IHandleMessages<LogVisit>
    {
        private readonly IVisitRepository _visitRepository;
        private readonly ILeadRepository _leadRepository;

        public LogVisitHandler(IVisitRepository visitRepository, ILeadRepository leadRepository)
        {
            _visitRepository = visitRepository;
            _leadRepository = leadRepository;
        }

        public void Handle(LogVisit message)
        {
            DomainEvents.Register<VisitMadeEvent>(VisitMadeEventHandler);
            var lead = _leadRepository.GetById(message.LeadId);
            Visit.Log(message.Id, lead, message.Start, message.End);
        }

        private void VisitMadeEventHandler(VisitMadeEvent @event)
        {
            _visitRepository.Save(@event.Source);
        }
    }
}
