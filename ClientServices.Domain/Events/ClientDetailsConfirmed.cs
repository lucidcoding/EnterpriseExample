using ClientServices.Domain.Common;
using ClientServices.Domain.Entities;

namespace ClientServices.Domain.Events
{
    public class ClientDetailsConfirmed : DomainEvent<Client>
    {
        public ClientDetailsConfirmed(Client client)
            : base(client)
        {
        }
    }
}
