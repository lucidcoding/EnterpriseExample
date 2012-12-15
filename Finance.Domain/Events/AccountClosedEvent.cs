using Finance.Domain.Common;
using Finance.Domain.Entities;

namespace Finance.Domain.Events
{
    public class AccountClosedEvent : DomainEvent<Account>
    {
        public AccountClosedEvent(Account account)
            : base(account)
        {
        }
    }
}
