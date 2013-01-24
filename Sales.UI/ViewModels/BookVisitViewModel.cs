using System;
using System.Web.Mvc;

namespace Sales.UI.ViewModels
{
    public class BookVisitViewModel
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid LeadId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public SelectList Consultants { get; set; }
        public Guid? ConsultantId { get; set; }
    }
}