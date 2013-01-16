using System;
using System.Collections.Generic;

namespace HumanResources.UI.ViewModels
{
    public class IndexHolidaysViewModel
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public IList<IndexHolidaysRecordViewModel> Records { get; set; } 
    }
}