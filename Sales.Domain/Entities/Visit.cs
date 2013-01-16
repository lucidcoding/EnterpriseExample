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
        public virtual Guid? ConsultantId { get; set; }
        public virtual bool Completed { get; set; }

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

        public static void Book(Guid id, Lead lead, DateTime start, DateTime end, Guid? consultantId)
        {
            var visit = new Visit
                            {
                                Id = id,
                                Lead = lead,
                                Start = start,
                                End = end,
                                ConsultantId = consultantId,
                                Completed = false
                            };

            DomainEvents.Raise(new VisitBookedEvent(visit));
        }

        public virtual void Complete()
        {
            Completed = true;
            DomainEvents.Raise(new VisitCompletedEvent(this));
        }
    }
}
