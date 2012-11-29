using System.Linq;
using HumanResources.Domain.Entities;
using HumanResources.WCF.DataTransferObjects;

namespace HumanResources.WCF.Mappers
{
    public class EmployeeDtoMapper
    {
        public EmployeeDto Map(Employee entity)
        {
            return new EmployeeDto
                       {
                           Id = entity.Id,
                           Forename = entity.Forename,
                           Surname = entity.Surname,
                           Joined = entity.Joined,
                           Left = entity.Left,
                           HolidayEntitlement = entity.HolidayEntitlement,
                           Department =
                               entity.Department != null ? new DepartmentDtoMapper().Map(entity.Department) : null,
                           FullName = entity.FullName
                       };
        }

        public EmployeeDto[] Map(Employee[] entities)
        {
            return entities.Select(Map).ToArray();
        }
    }
}