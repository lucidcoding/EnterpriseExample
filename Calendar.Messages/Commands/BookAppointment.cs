using System;
using NServiceBus;

namespace Calendar.Messages.Commands
{
    public class BookAppointment : IMessage
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
