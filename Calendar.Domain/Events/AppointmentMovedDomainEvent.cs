using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.Events
{
    public class AppointmentMovedDomainEvent : DomainEvent<Appointment>
    {
        public AppointmentMovedDomainEvent(Appointment appointment)
            : base(appointment)
        {
        }
    }
}
