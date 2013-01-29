using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class ClientActivatedDomainEvent : DomainEvent<Client>
    {
        public ClientActivatedDomainEvent(Client client)
            : base(client)
        {
        }
    }
}
