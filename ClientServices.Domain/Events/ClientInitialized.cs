using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class ClientInitialized : DomainEvent<Client>
    {
        public ClientInitialized(Client client)
            : base(client)
        {
        }
    }
}
