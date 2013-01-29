using Finance.Domain.Common;
using Finance.Domain.Entities;

namespace Finance.Domain.Events
{
    public class AccountOpenedDomainEvent : DomainEvent<Account>
    {
        public AccountOpenedDomainEvent(Account account)
            : base(account)
        {
        }
    }
}
