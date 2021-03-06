﻿using NServiceBus;
using Sales.Domain.Common;
using Sales.Domain.Entities;
using Sales.Domain.Events;
using Sales.Domain.RepositoryContracts;
using Sales.Messages.Commands;
using Sales.Messages.Replies;

namespace Sales.MessageHandlers.CommandHandlers
{
    public class BookVisitHandler : IHandleMessages<BookVisit>
    {
        private readonly IBus _bus;
        private readonly IVisitRepository _visitRepository;
        private readonly ILeadRepository _leadRepository;

        public BookVisitHandler(
            IBus bus, 
            IVisitRepository visitRepository, 
            ILeadRepository leadRepository)
        {
            _bus = bus;
            _visitRepository = visitRepository;
            _leadRepository = leadRepository;
        }

        public void Handle(BookVisit message)
        {
            DomainEvents.Register<VisitBookedDomainEvent>(VisitBookedDomainEventHandler);
            var lead = _leadRepository.GetById(message.LeadId);

            Visit.Book(
                message.Id, 
                message.AppointmentId,
                lead, 
                message.ConsultantId);

            _visitRepository.Flush();
            _bus.Return(ReturnCode.OK);
        }

        private void VisitBookedDomainEventHandler(VisitBookedDomainEvent @event)
        {
            _visitRepository.Save(@event.Source);
        }
    }
}
