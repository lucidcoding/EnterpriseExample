using System;
using System.Transactions;
using Calendar.Domain.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.RepositoryContracts;
using Calendar.Validation.Contracts;

namespace Calendar.Validation.Implementations
{
    public class AppointmentValidator : IAppointmentValidator
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentValidator(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public ValidationMessageCollection ValidateBook(Guid employeeId, Guid departmentId, DateTime start, DateTime end, string description)
        {
            ValidationMessageCollection messages;

            using (var transactionScope = new TransactionScope())
            {
                var employeesOtherAppointments = _appointmentRepository.GetByEmployeeId(employeeId);
                messages = Appointment.ValidateBook(employeesOtherAppointments, start, end);
                transactionScope.Complete();
            }

            return messages;
        }
    }
}
