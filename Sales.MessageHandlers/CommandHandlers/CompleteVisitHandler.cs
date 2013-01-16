using NServiceBus;
using Sales.Domain.Common;
using Sales.Domain.Events;
using Sales.Domain.RepositoryContracts;
using Sales.Messages.Commands;
using Sales.Messages.Replies;

namespace Sales.MessageHandlers.CommandHandlers
{
    public class CompleteVisitHandler : IHandleMessages<CompleteVisit>
    {
        private readonly IBus _bus;
        private readonly IVisitRepository _visitRepository;

        public CompleteVisitHandler(
            IBus bus,
            IVisitRepository visitRepository)
        {
            _bus = bus;
            _visitRepository = visitRepository;
        }

        public void Handle(CompleteVisit message)
        {
            DomainEvents.Register<VisitCompletedEvent>(VisitCompletedEventHandler);
            var visit = _visitRepository.GetById(message.Id);
            visit.Complete();
            _visitRepository.Flush();
            _bus.Return(ReturnCode.OK);
        }

        public void VisitCompletedEventHandler(VisitCompletedEvent @event)
        {
            _visitRepository.Update(@event.Source);
        }
    }
}
