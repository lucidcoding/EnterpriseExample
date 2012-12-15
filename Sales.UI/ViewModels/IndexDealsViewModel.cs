using System;
using System.Collections.Generic;

namespace Sales.UI.ViewModels
{
    public class IndexDealsViewModel
    {
        public Guid LeadId { get; set; }
        public string LeadName { get; set; }
        public IList<IndexDealsRecordViewModel> Records { get; set; }
    }
}