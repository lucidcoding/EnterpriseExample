using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class AgreementActivated : DomainEvent<Agreement>
    {
        public AgreementActivated(Agreement agreement)
            : base(agreement)
        {
        }
    }
}
