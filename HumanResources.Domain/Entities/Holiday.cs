using System;
using HumanResources.Domain.Common;
using HumanResources.Domain.Events;

namespace HumanResources.Domain.Entities
{
    public class Holiday : Entity<Guid>
    {
        public virtual Employee Employee { get; set; }
        public virtual Guid? AppointmentId { get; set; }
        public virtual int Length { get; set; }
        public virtual string Description { get; set; }

        public static ValidationMessageCollection ValidateBook(Employee employee, DateTime start, DateTime end, string description)
        {
            var messages = new ValidationMessageCollection();

            var length = (end - start).Days + 1;

            if(length > employee.RemainingHolidays)
            {
                messages.AddError("Length of holiday exceeds employee's remaining holidays.");
            }

            return messages;
        }

        public static void Book(Guid id, Employee employee, Guid appointmentId, DateTime start, DateTime end, string description)
        {
            var holiday = new Holiday
                              {
                                  Id = id,
                                  Employee = employee,
                                  AppointmentId = appointmentId,
                                  Length = (end - start).Days + 1,
                                  Description = description
                              };

            DomainEvents.Raise(new HolidayBookedEvent(holiday));
        }
    }
}
