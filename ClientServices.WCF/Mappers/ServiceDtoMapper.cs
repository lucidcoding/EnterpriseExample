using System.Linq;
using ClientServices.Domain.Entities;
using ClientServices.WCF.DataTransferObjects;

namespace ClientServices.WCF.Mappers
{
    public class ServiceDtoMapper
    {
        public ServiceDto Map(Service entity)
        {
            return new ServiceDto
                       {
                           Id = entity.Id.Value,
                           Name = entity.Name
                       };
        }

        public ServiceDto[] Map(Service[] entities)
        {
            return entities.Select(Map).ToArray();
        }
    }
}