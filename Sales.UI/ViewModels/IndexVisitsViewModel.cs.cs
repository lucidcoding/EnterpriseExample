using System;
using System.Collections.Generic;

namespace Sales.UI.ViewModels
{
    public class IndexVisitsViewModel
    {
        public Guid LeadId { get; set; }
        public string LeadName { get; set; }
        public IList<IndexVisitsRecordViewModel> BookedVisits { get; set; }
        public IList<IndexVisitsRecordViewModel> CompletedVisits { get; set; }
    }
}