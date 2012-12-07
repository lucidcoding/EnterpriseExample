using System;
using Sales.Domain.Common;
using Sales.Domain.Events;

namespace Sales.Domain.Entities
{
    public class Visit : Entity<Guid>
    {
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual Lead Lead { get; set; }

        public virtual DateTime Date
        {
            get { return Start.Date; }
        }

        public virtual TimeSpan StartTime
        {
            get { return Start.TimeOfDay; }
        }

        public virtual TimeSpan EndTime
        {
            get { return End.TimeOfDay; }
        }

        public static void Log(Guid id, Lead lead, DateTime start, DateTime end)
        {
            var visit = new Visit
                            {
                                Id = id,
                                Lead = lead,
                                Start = start,
                                End = end
                            };

            DomainEvents.Raise(new VisitMadeEvent(visit));
        }
    }
}
