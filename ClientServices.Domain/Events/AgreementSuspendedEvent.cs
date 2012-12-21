using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class AgreementSuspendedEvent : DomainEvent<Agreement>
    {
        public AgreementSuspendedEvent(Agreement agreement)
            : base(agreement)
        {
        }
    }
}
