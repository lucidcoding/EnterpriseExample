using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class AgreementCancelledEvent : DomainEvent<Agreement>
    {
        public AgreementCancelledEvent(Agreement agreement)
            : base(agreement)
        {
        }
    }
}
