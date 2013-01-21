using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.Events
{
    public class AppointmentBookedEvent : DomainEvent<Appointment>
    {
        public AppointmentBookedEvent(Appointment appointment) 
            : base(appointment)
        {
        }
    }
}
