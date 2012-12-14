using System.Collections.Generic;

namespace ClientServices.UI.ViewModels
{
    public class IndexClientsViewModel
    {
        public IList<IndexClientsRecordViewModel> InitializedClients { get; set; }
        public IList<IndexClientsRecordViewModel> ActiveClients { get; set; } 
    }
}