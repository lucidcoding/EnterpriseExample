using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class ClientLiasonUnassignedDomainEvent : DomainEvent<Client>
    {
        public ClientLiasonUnassignedDomainEvent(Client client)
            : base(client)
        {
        }
    }
}
