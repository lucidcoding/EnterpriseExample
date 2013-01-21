using Calendar.Domain.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.Events;
using Calendar.Domain.RepositoryContracts;
using Calendar.Messages.Commands;
using Calendar.Messages.Replies;
using NServiceBus;

namespace Calendar.MessageHandlers.CommandHandlers
{
    public class BookAppointmentHandler : IHandleMessages<BookAppointment>
    {
        private readonly IBus _bus;
        private readonly IAppointmentRepository _appointmentRepository;

        public BookAppointmentHandler(
            IBus bus,
            IAppointmentRepository appointmentRepository)
        {
            _bus = bus;
            _appointmentRepository = appointmentRepository;
        }

        public void Handle(BookAppointment message)
        {
            DomainEvents.Register<AppointmentBookedEvent>(AppointmentBookedEventHandler);
            var employeesOtherAppointments = _appointmentRepository.GetByEmployeeId(message.EmployeeId);

            Appointment.Book(
                message.Id, 
                message.EmployeeId, 
                message.DepartmentId, 
                employeesOtherAppointments,
                message.Start, 
                message.End);

            _appointmentRepository.Flush();
            _bus.Return(ReturnCode.OK);
        }

        public void AppointmentBookedEventHandler(AppointmentBookedEvent @event)
        {
            _appointmentRepository.Save(@event.Source);
        }
    }
}
