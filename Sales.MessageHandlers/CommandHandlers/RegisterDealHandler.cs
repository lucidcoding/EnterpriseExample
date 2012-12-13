using NServiceBus;
using Sales.Domain.Common;
using Sales.Domain.Entities;
using Sales.Domain.Events;
using Sales.Domain.RepositoryContracts;
using Sales.Messages.Commands;
using Sales.Messages.Replies;

namespace Sales.MessageHandlers.CommandHandlers
{
    public class RegisterDealHandler : IHandleMessages<RegisterDeal>
    {
        private readonly IBus _bus;
        private readonly IDealRepository _dealRepository;
        private readonly ILeadRepository _leadRepository;

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
            var lead = _leadRepository.GetById(message.LeadId);
            Deal.Register(message.Id, lead, message.ServiceIds, message.Value);
            _dealRepository.Flush();
            _bus.Return(ReturnCode.OK);
        }

        private void DealSignedEventHandler(DealSignedEvent @event)
        {
            _dealRepository.Save(@event.Source);
        }
    }
}
