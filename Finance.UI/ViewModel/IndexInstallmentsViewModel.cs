using System;
using System.Collections.Generic;

namespace Finance.UI.ViewModel
{
    public class IndexInstallmentsViewModel
    {
        public Guid AccountId { get; set; }
        public IList<IndexInstallmentsRecordViewModel> Records { get; set; }
    }
}