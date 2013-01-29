using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.Events
{
    public class AppointmentBookedDomainEvent : DomainEvent<Appointment>
    {
        public AppointmentBookedDomainEvent(Appointment appointment) 
            : base(appointment)
        {
        }
    }
}
