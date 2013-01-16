using System;

namespace HumanResources.UI.ViewModels
{
    public class BookHolidayViewModel
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
    }
}