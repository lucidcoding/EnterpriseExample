using System;
using System.Web.Mvc;

namespace HumanResources.UI.ViewModels
{
    public class IndexEmployeesViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public int HolidayEntitlement { get; set; }
    }
}