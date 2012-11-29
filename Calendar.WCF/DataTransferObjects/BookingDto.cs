using System;

namespace Calendar.WCF.DataTransferObjects
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public BookingTypeDto BookingType { get; set; }
    }
}
