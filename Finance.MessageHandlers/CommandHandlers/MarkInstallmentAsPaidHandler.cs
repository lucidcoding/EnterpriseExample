﻿using Finance.Domain.Common;
using Finance.Domain.Events;
using Finance.Domain.RepositoryContracts;
using Finance.Messages.Commands;
using Finance.Messages.Replies;
using NServiceBus;

namespace Finance.MessageHandlers.CommandHandlers
{
    public class MarkInstallmentAsPaidHandler : IHandleMessages<MarkInstallmentAsPaid>
    {
        private readonly IBus _bus;
        private readonly IInstallmentRepository _installmentRepository;

        public MarkInstallmentAsPaidHandler(
            IBus bus,
            IInstallmentRepository installmentRepository)
        {
            _bus = bus;
            _installmentRepository = installmentRepository;
        }

        public void Handle(MarkInstallmentAsPaid message)
        {
            DomainEvents.Register<InstallmentPaidDomainEvent>(InstallmentPaidDomainEventHandler);
            var installment = _installmentRepository.GetById(message.Id);
            installment.MarkAsPaid();
            _installmentRepository.Flush();
            _bus.Return(ReturnCode.OK);
        }

        public void InstallmentPaidDomainEventHandler(InstallmentPaidDomainEvent @event)
        {
            _installmentRepository.Save(@event.Source);
        }
    }
}
