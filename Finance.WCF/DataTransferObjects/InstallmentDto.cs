using System;
using Finance.Domain.Enumerations;

namespace Finance.WCF.DataTransferObjects
{
    public class InstallmentDto
    {
        public Guid? Id { get; set; }
        public DateTime DueDate { get; set; }
        public int Amount { get; set; }
        public bool Paid { get; set; }
        public AccountDto Account { get; set; }
        public InstallmentStatus Status { get; set; }
    }
}