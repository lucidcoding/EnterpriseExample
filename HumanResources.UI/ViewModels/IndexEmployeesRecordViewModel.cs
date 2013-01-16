using System;
using System.Web.Mvc;

namespace HumanResources.UI.ViewModels
{
    public class IndexEmployeesRecordViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public int HolidayEntitlement { get; set; }
        public int TotalHolidays { get; set; }
        public int RemainingHolidays { get; set; }
    }
}