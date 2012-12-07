using System;

namespace Sales.UI.ViewModels
{
    public class IndexLeadsRecordViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string AssignedToConsultantName { get; set; }
    }
}