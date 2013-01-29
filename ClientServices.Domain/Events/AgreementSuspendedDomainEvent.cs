using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class AgreementSuspendedDomainEvent : DomainEvent<Agreement>
    {
        public AgreementSuspendedDomainEvent(Agreement agreement)
            : base(agreement)
        {
        }
    }
}
