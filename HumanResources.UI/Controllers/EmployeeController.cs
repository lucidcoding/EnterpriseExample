using System;
using System.Linq;
using System.Web.Mvc;
using HumanResources.Application.Contracts;
using HumanResources.UI.ViewModels;

namespace HumanResources.UI.Controllers
{
    /// <remarks>
    /// I'm well aware that there are some design issues here, such as the fact that it needs to get the employee entity
    /// twice, once to display holiday entitlement/remaining, and another time to get calendar entries. Also,
    /// when getting the list of employees, it gets all calendar entries for each one. This can be avoided but I'm leaving
    /// it for the sake of speed and simplicity. It's EDA I'm demonstrating here, not fine tuning NHibernate behavior.
    /// </remarks>
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public ActionResult Index(Guid? employeeId)
        {
            var employees = _employeeService.GetAll();

            var viewModel = employees.Select(x => new IndexEmployeesViewModel
                                                      {
                                                          Id = x.Id.Value,
                                                          FullName = x.FullName,
                                                          Department = x.Department != null
                                                                           ? x.Department.Name
                                                                           : null,
                                                          HolidayEntitlement = x.HolidayEntitlement
                                                      }).ToList();

            return View(viewModel);
        }
    }
}
