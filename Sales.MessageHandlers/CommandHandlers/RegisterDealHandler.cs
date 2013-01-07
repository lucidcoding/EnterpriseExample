using System;
using NServiceBus;
using Sales.Domain.Common;
using Sales.Domain.Entities;
using Sales.Domain.Events;
using Sales.Domain.RepositoryContracts;
using Sales.Messages.Commands;
using Sales.Messages.Events;
using Sales.Messages.Replies;

namespace Sales.MessageHandlers.CommandHandlers
{
    public class RegisterDealHandler : IHandleMessages<RegisterDeal>
    {
        private readonly IBus _bus;
        private readonly IDealRepository _dealRepository;
        private readonly ILeadRepository _leadRepository;
        private Guid _correlationId;

        public RegisterDealHandler(
            IBus bus, 
            IDealRepository dealRepository, 
            ILeadRepository leadRepository)
        {
            _bus = bus;
            _dealRepository = dealRepository;
            _leadRepository = leadRepository;
        }

        public void Handle(RegisterDeal message)
        {
            DomainEvents.Register<DealSignedEvent>(DealSignedEventHandler);
            DomainEvents.Register<LeadSignedUpEventEvent>(LeadSignedUpEventHandler);
            _correlationId = message.CorrelationId;
            var lead = _leadRepository.GetById(message.LeadId);
            Deal.Register(message.DealId, lead, message.Value);
            _dealRepository.Flush();
            _bus.Return(ReturnCode.OK);
        }

        private void DealSignedEventHandler(DealSignedEvent @event)
        {
            _dealRepository.Save(@event.Source);

            _bus.Publish(new DealRegistered
                             {
                                 Id = @event.Source.Id.Value,
                                 LeadId = @event.Source.Lead.Id.Value,
                                 MadeByConsultantId = @event.Source.MadeByConsultantId,
                                 Value = @event.Source.Value,
                                 Commission = @event.Source.Value
                             });
        }

        private void LeadSignedUpEventHandler(LeadSignedUpEventEvent @event)
        {
            _bus.Publish(new LeadSignedUp
                             {
                                 CorrelationId = _correlationId,
                                 LeadId = @event.Source.Id.Value,
                                 Name = @event.Source.Name,
                                 Address1 = @event.Source.Address1,
                                 Address2 = @event.Source.Address2,
                                 Address3 = @event.Source.Address3,
                                 PhoneNumber = @event.Source.PhoneNumber
                             });
        }
    }
}
