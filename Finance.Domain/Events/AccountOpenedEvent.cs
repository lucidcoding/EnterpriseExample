using Finance.Domain.Common;
using Finance.Domain.Entities;

namespace Finance.Domain.Events
{
    public class AccountOpenedEvent : DomainEvent<Account>
    {
        public AccountOpenedEvent(Account account)
            : base(account)
        {
        }
    }
}
