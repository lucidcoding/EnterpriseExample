using System;
using Sales.Domain.Common;

namespace Sales.Domain.Entities
{
    public class DealService : Entity<Guid>
    {
        public virtual Guid ServiceId { get; set; }
    }
}
