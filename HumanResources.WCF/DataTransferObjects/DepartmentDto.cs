using System;

namespace HumanResources.WCF.DataTransferObjects
{
    public class DepartmentDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public EmployeeDto Manager { get; set; }
    }
}