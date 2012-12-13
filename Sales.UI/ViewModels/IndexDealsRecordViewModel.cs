using System;

namespace Sales.UI.ViewModels
{
    public class IndexDealsRecordViewModel
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string MadeByConsultant { get; set; }
        public int Value { get; set; }
    }
}