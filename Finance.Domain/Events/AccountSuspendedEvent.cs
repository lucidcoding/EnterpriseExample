using Finance.Domain.Common;
using Finance.Domain.Entities;

namespace Finance.Domain.Events
{
    public class AccountSuspendedEvent : DomainEvent<Account>
    {
        public AccountSuspendedEvent(Account account)
            : base(account)
        {
        }
    }
}
