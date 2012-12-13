using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class AgreementCancelled : DomainEvent<Agreement>
    {
        public AgreementCancelled(Agreement agreement)
            : base(agreement)
        {
        }
    }
}
