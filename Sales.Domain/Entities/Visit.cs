using System;
using Sales.Domain.Common;
using Sales.Domain.Events;

namespace Sales.Domain.Entities
{
    public class Visit : Entity<Guid>
    {
        public virtual Guid? AppointmentId { get; set; }
        public virtual Lead Lead { get; set; }
        public virtual Guid? ConsultantId { get; set; }
        public virtual bool Completed { get; set; }

        public static void Book(Guid id, Guid? appointmentId, Lead lead, Guid? consultantId)
        {
            var visit = new Visit
                            {
                                Id = id,
                                AppointmentId = appointmentId,
                                Lead = lead,
                                ConsultantId = consultantId,
                                Completed = false
                            };

            DomainEvents.Raise(new VisitBookedDomainEvent(visit));
        }

        public virtual void Complete()
        {
            Completed = true;
            DomainEvents.Raise(new VisitCompletedDomainEvent(this));
        }
    }
}
