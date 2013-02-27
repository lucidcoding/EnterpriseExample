using System.Linq;
using HumanResources.Domain.Entities;
using HumanResources.WCF.DataTransferObjects;

namespace HumanResources.WCF.Mappers
{
    public class DepartmentDtoMapper
    {
        public DepartmentDto Map(Department entity)
        {
            return new DepartmentDto
                       {
                           Id = entity.Id,
                           Name = entity.Name
                       };
        }

        public DepartmentDto MapWithManager(Department entity)
        {
            var dto = Map(entity);
            dto.Manager = entity.Manager != null ? new EmployeeDtoMapper().Map(entity.Manager) : null;
            return dto;
        }

        public DepartmentDto[] Map(Department[] entities)
        {
            return entities.Select(Map).ToArray();
        }

        public DepartmentDto[] MapWithManager(Department[] entities)
        {
            return entities.Select(MapWithManager).ToArray();
        }
    }
}