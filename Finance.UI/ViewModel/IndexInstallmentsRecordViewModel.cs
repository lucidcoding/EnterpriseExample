using System;
using Finance.Domain.Enumerations;

namespace Finance.UI.ViewModel
{
    public class IndexInstallmentsRecordViewModel
    {
        public Guid Id { get; set; }
        public DateTime DueDate { get; set; }
        public int Amount { get; set; }
        public InstallmentStatus Status { get; set; }
    }
}