using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class ClientInitializedDomainEvent : DomainEvent<Client>
    {
        public ClientInitializedDomainEvent(Client client)
            : base(client)
        {
        }
    }
}
