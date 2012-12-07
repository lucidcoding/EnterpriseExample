using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using HumanResources.Application.Contracts;
using HumanResources.Messages.Commands;
using HumanResources.Messages.Replies;
using HumanResources.UI.ViewModels;
using NServiceBus;

namespace HumanResources.UI.Controllers
{
    /// <remarks>
    /// I'm well aware that there are some design issues here, such as the fact that it needs to get the employee entity
    /// twice, once to display holiday entitlement/remaining, and another time to get calendar entries. Also,
    /// when getting the list of employees, it gets all calendar entries for each one. This can be avoided but I'm leaving
    /// it for the sake of speed and simplicity. It's EDA I'm demonstrating here, not fine tuning NHibernate behavior.
    /// </remarks>
    public class EmployeeController : AsyncController
    {
        private readonly IBus _bus;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IBus bus, IEmployeeService employeeService)
        {
            _bus = bus;
            _employeeService = employeeService;
        }

        public ActionResult Index(Guid? employeeId)
        {
            var employees = _employeeService.GetCurrent();

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

        public ActionResult Register()
        {
            return View("ComingSoon");
        }

        public ActionResult Edit()
        {
            return View("ComingSoon");
        }

        public void MarkAsLeftAsync(Guid id)
        {
            var command = new MarkEmployeeAsLeft {Id = id};
            _bus.Send(command).Register<ReturnCode>(status => AsyncManager.Parameters["returnCode"] = status);
        }

        public ActionResult MarkAsLeftCompleted(ReturnCode returnCode)
        {
            return RedirectToAction("Index");
        }
    }
}
