using System;

namespace Sales.UI.ViewModels
{
    public class IndexVisitsRecordViewModel
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string ConsultantName { get; set; }
    }
}