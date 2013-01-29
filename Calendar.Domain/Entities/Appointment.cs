using System;
using System.Collections.Generic;
using System.Linq;
using Calendar.Domain.Common;
using Calendar.Domain.Events;

namespace Calendar.Domain.Entities
{
    public class Appointment : Entity<Guid>
    {
        public virtual Guid EmployeeId { get; set; }
        public virtual Guid DepartmentId { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }

        private static ValidationMessageCollection ValidateBookMove(DateTime start, DateTime end, IEnumerable<Appointment> employeesOtherAppointments, Appointment bookingBeingUpdated)
        {
            var validationMessages = new ValidationMessageCollection();

            if (start == default(DateTime) || end == default(DateTime))
                validationMessages.AddError("Start and end time not correctly set.");

            if (!validationMessages.IsValid)
                return validationMessages;

            if (start > end)
                validationMessages.AddError("Start date is after end date.");

            if (!validationMessages.IsValid)
                return validationMessages;

            var matchingTimeAllocations = (from appointment in employeesOtherAppointments
                                           where (bookingBeingUpdated == null || bookingBeingUpdated != appointment)
                                              && ((start >= appointment.Start && start <= appointment.End)
                                              || (end >= appointment.Start && end <= appointment.End)
                                              || (start <= appointment.Start && end >= appointment.End))
                                           select appointment)
               .ToList();

            if (matchingTimeAllocations.Any())
                validationMessages.AddError("Appointment clashes with other appointments for employee.");

            return validationMessages;
        }

        public static ValidationMessageCollection ValidateBook(IEnumerable<Appointment> employeesOtherAppointments, DateTime start, DateTime end)
        {
            return ValidateBookMove(start, end, employeesOtherAppointments, null);
        }

        public static Appointment Book(Guid id, Guid employeeId, Guid departmentId, IEnumerable<Appointment> employeesOtherAppointments, DateTime start, DateTime end)
        {
            var appointment = new Appointment
            {
                Id = id,
                EmployeeId = employeeId,
                DepartmentId = departmentId,
                Start = start,
                End = end
            };

            DomainEvents.Raise(new AppointmentBookedDomainEvent(appointment));
            return appointment;
        }

        public virtual ValidationMessageCollection ValidateMove(IEnumerable<Appointment> employeesOtherAppointments, DateTime start, DateTime end)
        {
            return ValidateBookMove(start, end, employeesOtherAppointments, this);
        }

        public virtual void Move(IEnumerable<Appointment> employeesOtherAppointments, DateTime start, DateTime end)
        {
            Start = start;
            End = end;
            DomainEvents.Raise(new AppointmentMovedDomainEvent(this));
        }
    }
}
