using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class AgreementCancelledDomainEvent : DomainEvent<Agreement>
    {
        public AgreementCancelledDomainEvent(Agreement agreement)
            : base(agreement)
        {
        }
    }
}
