using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class ClientInitializedEvent : DomainEvent<Client>
    {
        public ClientInitializedEvent(Client client)
            : base(client)
        {
        }
    }
}
