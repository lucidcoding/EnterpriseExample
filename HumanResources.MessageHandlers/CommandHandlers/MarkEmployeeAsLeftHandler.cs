﻿using System;
using HumanResources.Domain.Common;
using HumanResources.Domain.Events;
using HumanResources.Domain.RepositoryContracts;
using HumanResources.Messages.Commands;
using HumanResources.Messages.Events;
using NServiceBus;

namespace HumanResources.MessageHandlers.CommandHandlers
{
    public class MarkEmployeeAsLeftHandler : IHandleMessages<MarkEmployeeAsLeft>
    {
        public IBus _bus;
        private readonly IEmployeeRepository _employeeRepository;

        public MarkEmployeeAsLeftHandler(IBus bus, IEmployeeRepository employeeRepository)
        {
            _bus = bus;
            _employeeRepository = employeeRepository;
        }

        public void Handle(MarkEmployeeAsLeft message)
        {
            DomainEvents.Register<EmployeeLeftEvent>(EmployeeLeftHandler);
            var employee = _employeeRepository.GetById(message.Id);
            employee.MarkAsLeft();
            _employeeRepository.Flush();
        }

        private void EmployeeLeftHandler(EmployeeLeftEvent @event)
        {
            _employeeRepository.Save(@event.Source);

            var makeBookingCommand = new EmployeeLeft
            {
                Id = @event.Source.Id.Value,
                Left = @event.Source.Left,
                DepartmentId = @event.Source.Department != null ? @event.Source.Department.Id : default(Guid?)
            };

            _bus.Publish(makeBookingCommand);
        }
    }
}
