using System;
using System.Collections.Generic;
using System.Transactions;
using Calendar.Messages.Commands;
using HumanResources.Application.Contracts;
using HumanResources.Application.Requests;
using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.Global;
using HumanResources.Domain.RepositoryContracts;
using NServiceBus;

namespace HumanResources.Application.Implementations
{
    public class HolidayService : IHolidayService
    {
        private readonly IBus _bus;
        private readonly IHolidayRepository _holidayRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public HolidayService(
            IBus bus,
            IHolidayRepository holidayRepository,
            IEmployeeRepository employeeRepository)
        {
            _bus = bus;
            _holidayRepository = holidayRepository;
            _employeeRepository = employeeRepository;
        }

        //todo: settle on using list or ilist or ienumerable or arrays etc.
        public IList<Holiday> GetByIds(List<Guid> ids)
        {
            using (var transactionScope = new TransactionScope())
            {
                var holidays = _holidayRepository.GetByIds(ids);
                transactionScope.Complete();
                return holidays;
            }
        }

        public ValidationMessageCollection ValidateBook(BookHolidayRequest request)
        {
            using (var transactionScope = new TransactionScope())
            {
                var employee = _employeeRepository.GetById(request.EmployeeId);
                var validationMessages = Holiday.ValidateBook(employee, request.Start, request.End);
                transactionScope.Complete();
                return validationMessages;
            }
        }

        public void Book(BookHolidayRequest request)
        {
            using (var transactionScope = new TransactionScope())
            {
                var employee = _employeeRepository.GetById(request.EmployeeId);
                DomainEvents.Register<HolidayBookedEvent>(HolidayBooked);
                Holiday.Book(request.Id, employee, request.Start, request.End);
                _employeeRepository.Flush();
                transactionScope.Complete();
            }
        }

        private void HolidayBooked(HolidayBookedEvent @event)
        {
            _holidayRepository.Save(@event.Source);

            var makeBookingCommand = new MakeBooking
            {
                Id = @event.Source.Id.Value,
                EmployeeId = @event.Source.Employee.Id.Value,
                Start = @event.Source.Start,
                End = @event.Source.End,
                BookingTypeId = Constants.HolidayBookingTypeId
            };

            _bus.Send(makeBookingCommand);
        }

        public ValidationMessageCollection ValidateUpdate(UpdateHolidayRequest request)
        {
            using (var transactionScope = new TransactionScope())
            {
                var holiday = _holidayRepository.GetById(request.Id);
                var validationMessages = holiday.ValidateUpdate(request.Start, request.End);
                transactionScope.Complete();
                return validationMessages;
            }
        }

        public void Update(UpdateHolidayRequest request)
        {
            using (var transactionScope = new TransactionScope())
            {
                var holiday = _holidayRepository.GetById(request.Id);
                DomainEvents.Register<HolidayUpdatedEvent>(HolidayUpdated);
                holiday.Update(request.Start, request.End);
                _employeeRepository.Flush();
                transactionScope.Complete();
            }
        }

        private void HolidayUpdated(HolidayUpdatedEvent @event)
        {
            _holidayRepository.Update(@event.Source);

            var updateBookingCommand = new UpdateBooking
            {
                Id = @event.Source.Id.Value,
                Start = @event.Source.Start,
                End = @event.Source.End
            };

            _bus.Send(updateBookingCommand);
        }
    }
}
