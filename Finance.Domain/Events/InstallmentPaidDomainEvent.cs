using Finance.Domain.Common;
using Finance.Domain.Entities;

namespace Finance.Domain.Events
{
    public class InstallmentPaidDomainEvent : DomainEvent<Installment>
    {
        public InstallmentPaidDomainEvent(Installment installment)
            : base(installment)
        {
        }
    }
}
