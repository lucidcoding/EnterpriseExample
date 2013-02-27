using System.Linq;
using Sales.Domain.Entities;
using Sales.WCF.DataTransferObjects;

namespace Sales.WCF.Mappers
{
    public class LeadDtoMapper
    {
        public LeadDto Map(Lead entity)
        {
            return new LeadDto
                       {
                           Id = entity.Id,
                           Name = entity.Name,
                           Address1 = entity.Address1,
                           Address2 = entity.Address2,
                           Address3 = entity.Address3,
                           PhoneNumber = entity.PhoneNumber,
                           EmailAddress = entity.EmailAddress,
                           AssignedToConsultantId = entity.AssignedToConsultantId,
                           SignedUp = entity.SignedUp
                       };
        }

        public LeadDto[] Map(Lead[] entities)
        {
            return entities.Select(Map).ToArray();
        }
    }
}