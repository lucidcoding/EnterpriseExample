using System;

namespace HumanResources.WCF.DataTransferObjects
{
    public class EmployeeDto
    {
        public Guid? Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime? Joined { get; set; }
        public DateTime? Left { get; set; }
        public int HolidayEntitlement { get; set; }
        public DepartmentDto Department { get; set; }
        public string EmailAddress { get; set; } 
        public string FullName { get; set; }
    }
}