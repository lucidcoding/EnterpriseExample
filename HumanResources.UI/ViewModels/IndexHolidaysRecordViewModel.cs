using System;

namespace HumanResources.UI.ViewModels
{
    public class IndexHolidaysRecordViewModel
    {
        public Guid? Id { get; set; }
        public string AppointmentType { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
    }
}