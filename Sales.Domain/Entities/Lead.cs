﻿using System;
using Sales.Domain.Common;

namespace Sales.Domain.Entities
{
    public class Lead : Entity<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        public virtual Guid ConsultantIdAssignedTo { get; set; }
    }
}
