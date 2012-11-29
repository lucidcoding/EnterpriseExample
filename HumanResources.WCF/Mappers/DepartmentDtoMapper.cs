using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public DepartmentDto[] Map(Department[] entities)
        {
            return entities.Select(Map).ToArray();
        }
    }
}