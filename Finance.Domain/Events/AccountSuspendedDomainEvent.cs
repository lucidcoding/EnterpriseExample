using Finance.Domain.Common;
using Finance.Domain.Entities;

namespace Finance.Domain.Events
{
    public class AccountSuspendedDomainEvent : DomainEvent<Account>
    {
        public AccountSuspendedDomainEvent(Account account)
            : base(account)
        {
        }
    }
}
