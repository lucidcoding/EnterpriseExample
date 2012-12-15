using Finance.Domain.Common;
using Finance.Domain.Entities;

namespace Finance.Domain.Events
{
    public class InstallmentPaidEvent : DomainEvent<Installment>
    {
        public InstallmentPaidEvent(Installment installment)
            : base(installment)
        {
        }
    }
}
