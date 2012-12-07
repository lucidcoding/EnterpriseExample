using System;

namespace Sales.UI.ViewModels
{
    public class LogVisitViewModel
    {
        public Guid Id { get; set; }
        public Guid LeadId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}