using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class AgreementActivatedDomainEvent : DomainEvent<Agreement>
    {
        public AgreementActivatedDomainEvent(Agreement agreement)
            : base(agreement)
        {
        }
    }
}
