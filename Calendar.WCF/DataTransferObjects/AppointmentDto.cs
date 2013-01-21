using System;

namespace Calendar.WCF.DataTransferObjects
{
    public class AppointmentDto
    {
        public Guid? Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}