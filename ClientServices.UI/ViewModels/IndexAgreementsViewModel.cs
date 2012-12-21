using System;
using System.Collections.Generic;

namespace ClientServices.UI.ViewModels
{
    public class IndexAgreementsViewModel
    {
        public Guid ClientId { get; set; }
        public IList<IndexAgreementsRecordViewModel> Records { get; set; } 
    }
}