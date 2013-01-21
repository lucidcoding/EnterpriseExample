using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.Events
{
    public class AppointmentMovedEvent : DomainEvent<Appointment>
    {
        public AppointmentMovedEvent(Appointment appointment)
            : base(appointment)
        {
        }
    }
}
