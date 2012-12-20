using System.Linq;
using ClientServices.Domain.Entities;
using ClientServices.WCF.DataTransferObjects;

namespace ClientServices.WCF.Mappers
{
    public class ClientDtoMapper
    {
        public ClientDto Map(Client entity)
        {
            return new ClientDto
                       {
                           Id = entity.Id.Value,
                           Name = entity.Name,
                           Reference = entity.Reference,
                           Address1 = entity.Address1,
                           Address2 = entity.Address2,
                           Address3 = entity.Address3,
                           PhoneNumber = entity.PhoneNumber,
                           LiasonEmployeeId = entity.LiasonEmployeeId
                       };
        }

        public ClientDto[] Map(Client[] entities)
        {
            return entities.Select(Map).ToArray();
        }
    }
}