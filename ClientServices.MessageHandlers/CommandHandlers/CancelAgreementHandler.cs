using ClientServices.Domain.Common;
using ClientServices.Domain.Events;
using ClientServices.Domain.RepositoryContracts;
using ClientServices.Messages.Commands;
using ClientServices.Messages.Events;
using NServiceBus;

namespace ClientServices.MessageHandlers.CommandHandlers
{
    public class CancelAgreementHandler : IHandleMessages<CancelAgreement>
    {
        private readonly IBus _bus;
        private readonly IAgreementRepository _agreementRepository;

        public CancelAgreementHandler(
            IBus bus, 
            IAgreementRepository agreementRepository)
        {
            _bus = bus;
            _agreementRepository = agreementRepository;
        }

        public void Handle(CancelAgreement message)
        {
            DomainEvents.Register<AgreementCancelledEvent>(AgreementCancelledEventHandler);
            var agreement = _agreementRepository.GetById(message.Id);
            agreement.Cancel();
            _agreementRepository.Flush();
        }

        public void AgreementCancelledEventHandler(AgreementCancelledEvent @event)
        {
            _agreementRepository.Update(@event.Source);

            var agreementCancelled = new AgreementCancelled
                                         {
                                             Id = @event.Source.Id.Value
                                         };

            _bus.Publish(agreementCancelled);
        }
    }
}
