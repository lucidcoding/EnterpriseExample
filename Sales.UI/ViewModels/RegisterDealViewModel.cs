using System;
using System.Collections.Generic;

namespace Sales.UI.ViewModels
{
    public class RegisterDealViewModel
    {
        public Guid Id { get; set; }
        public Guid LeadId { get; set; }
        public IList<Guid> ServiceIds { get; set; }
        public int Value { get; set; }
    }
}