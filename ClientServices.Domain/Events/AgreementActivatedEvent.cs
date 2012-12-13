using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class AgreementActivatedEvent : DomainEvent<Agreement>
    {
        public AgreementActivatedEvent(Agreement agreement)
            : base(agreement)
        {
        }
    }
}
