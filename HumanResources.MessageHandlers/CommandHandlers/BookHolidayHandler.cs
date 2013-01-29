using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.RepositoryContracts;
using HumanResources.Messages.Commands;
using HumanResources.Messages.Replies;
using NServiceBus;

namespace HumanResources.MessageHandlers.CommandHandlers
{
    public class BookHolidayHandler : IHandleMessages<BookHoliday>
    {
        private readonly IBus _bus;
        private readonly IHolidayRepository _holidayRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BookHolidayHandler(
            IBus bus,
            IHolidayRepository holidayRepository,
            IEmployeeRepository employeeRepository)
        {
            _bus = bus;
            _holidayRepository = holidayRepository;
            _employeeRepository = employeeRepository;
        }

        public void Handle(BookHoliday message)
        {
            DomainEvents.Register<HolidayBookedDomainEvent>(HolidayBookedDomainEventHandler);
            var employee = _employeeRepository.GetById(message.EmployeeId);

            Holiday.Book(
                message.Id,
                employee,
                message.AppointmentId,
                message.Start,
                message.End,
                message.Description);

            _employeeRepository.Flush();
            _bus.Return(ReturnCode.OK);
        }

        public void HolidayBookedDomainEventHandler(HolidayBookedDomainEvent @event)
        {
            _holidayRepository.Save(@event.Source);
        }
    }
}
