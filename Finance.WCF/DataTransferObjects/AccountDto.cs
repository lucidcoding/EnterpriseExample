using System;
using Finance.Domain.Enumerations;

namespace Finance.WCF.DataTransferObjects
{
    public class AccountDto
    {
        public Guid? Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid AgreementId { get; set; }
        public DateTime Expiry { get; set; }
        public int Value { get; set; }
        public InstallmentDto[] Installments { get; set; }
        public AccountStatus Status { get; set; }
    }
}