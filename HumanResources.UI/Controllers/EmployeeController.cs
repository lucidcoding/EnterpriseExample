using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using HumanResources.Domain.Entities;
using HumanResources.Domain.RepositoryContracts;
using HumanResources.Messages.Commands;
using HumanResources.Messages.Replies;
using HumanResources.UI.ViewModels;
using NServiceBus;

namespace HumanResources.UI.Controllers
{
    public class EmployeeController : AsyncController
    {
        private readonly IBus _bus;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IBus bus, IEmployeeRepository employeeRepository)
        {
            _bus = bus;
            _employeeRepository = employeeRepository;
        }

        public ActionResult Index(Guid? employeeId)
        {
            IList<Employee> employees;
            
            using (var transactionScope = new TransactionScope())
            {
                employees = _employeeRepository.GetCurrent();
                transactionScope.Complete();
            }

            var viewModel = employees.Select(employee => new IndexEmployeesRecordViewModel
                                                             {
                                                                 Id = employee.Id.Value,
                                                                 FullName = employee.FullName,
                                                                 Department = employee.Department != null
                                                                                  ? employee.Department.Name
                                                                                  : null,
                                                                 HolidayEntitlement = employee.HolidayEntitlement,
                                                                 TotalHolidays = employee.TotalHolidays,
                                                                 RemainingHolidays = employee.RemainingHolidays
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
            _bus.Send(command).Register<ReturnCode>(returnCode => AsyncManager.Parameters["returnCode"] = returnCode);
        }

        public ActionResult MarkAsLeftCompleted(ReturnCode returnCode)
        {
            return RedirectToAction("Index");
        }
    }
}
