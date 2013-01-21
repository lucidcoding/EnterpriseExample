using System;

namespace Calendar.WCF.Requests
{
    public class ValidateBookRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
    }
}