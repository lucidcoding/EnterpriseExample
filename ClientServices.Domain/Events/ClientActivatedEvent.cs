using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class ClientActivatedEvent : DomainEvent<Client>
    {
        public ClientActivatedEvent(Client client)
            : base(client)
        {
        }
    }
}
