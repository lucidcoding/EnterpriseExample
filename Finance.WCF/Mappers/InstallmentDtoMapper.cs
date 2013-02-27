using System.Linq;
using Finance.Domain.Entities;
using Finance.WCF.DataTransferObjects;

namespace Finance.WCF.Mappers
{
    public class InstallmentDtoMapper
    {
        public InstallmentDto Map(Installment entity)
        {
            return new InstallmentDto
                       {
                           Id = entity.Id,
                           DueDate = entity.DueDate,
                           Amount = entity.Amount,
                           Paid = entity.Paid,
                           Status = entity.Status
                       };
        }

        public InstallmentDto MapWithAccount(Installment entity)
        {
            var dto = Map(entity);
            dto.Account = entity.Account != null ? new AccountDtoMapper().Map(entity.Account) : null;
            return dto;
        }

        public InstallmentDto[] Map(Installment[] entities)
        {
            return entities.Select(Map).ToArray();
        }

        public InstallmentDto[] MapWithAccount(Installment[] entities)
        {
            return entities.Select(MapWithAccount).ToArray();
        }
    }
}