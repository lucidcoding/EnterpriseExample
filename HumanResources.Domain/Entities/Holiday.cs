using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HumanResources.Domain.Common;
using HumanResources.Domain.Events;

namespace HumanResources.Domain.Entities
{
    public class Holiday : Entity<Guid>
    {
        public virtual Employee Employee { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual string Description { get; set; }

        public virtual int Length
        {
            get { return (End - Start).Days + 1; }
        }

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

        public static void Book(Guid id, Employee employee, DateTime start, DateTime end, string description)
        {
            var holiday = new Holiday
                              {
                                  Id = id,
                                  Employee = employee,
                                  Start = start,
                                  End = end,
                                  Description = description
                              };

            DomainEvents.Raise(new HolidayBookedEvent(holiday));
        }
    }
}
