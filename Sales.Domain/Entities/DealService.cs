using System;
using Sales.Domain.Common;

namespace Sales.Domain.Entities
{
    public class DealService : Entity<Guid>
    {
        public Guid ServiceId { get; set; }
    }
}
