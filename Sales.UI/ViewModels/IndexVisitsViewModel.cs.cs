using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sales.UI.ViewModels
{
    public class IndexVisitsViewModel
    {
        public Guid LeadId { get; set; }
        public string LeadName { get; set; }
        public IList<IndexVisitsRecordViewModel> Records { get; set; }
    }
}