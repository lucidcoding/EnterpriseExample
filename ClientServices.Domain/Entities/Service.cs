using System;
using ClientServices.Domain.Common;

namespace ClientServices.Domain.Entities
{
    public class Service : Entity<Guid>
    {
        public virtual string Name { get; set; }
    }
}
