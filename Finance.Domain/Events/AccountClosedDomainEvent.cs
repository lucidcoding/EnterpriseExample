using Finance.Domain.Common;
using Finance.Domain.Entities;

namespace Finance.Domain.Events
{
    public class AccountClosedDomainEvent : DomainEvent<Account>
    {
        public AccountClosedDomainEvent(Account account)
            : base(account)
        {
        }
    }
}
