using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class ClientLiasonUnassignedEvent : DomainEvent<Client>
    {
        public ClientLiasonUnassignedEvent(Client client)
            : base(client)
        {
        }
    }
}
