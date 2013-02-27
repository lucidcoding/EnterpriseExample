using System.Linq;
using Finance.Domain.Entities;
using Finance.WCF.DataTransferObjects;

namespace Finance.WCF.Mappers
{
    public class AccountDtoMapper
    {
        public AccountDto Map(Account entity)
        {
            return new AccountDto
                       {
                           Id = entity.Id,
                           ClientId = entity.ClientId,
                           AgreementId = entity.AgreementId,
                           Expiry = entity.Expiry,
                           Value = entity.Value,
                           Status = entity.Status
                       };
        }

        public AccountDto[] Map(Account[] entities)
        {
            return entities.Select(Map).ToArray();
        }
    }
}