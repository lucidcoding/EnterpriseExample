using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using HumanResources.Domain.Entities;
using HumanResources.Domain.RepositoryContracts;
using HumanResources.Messages.Commands;
using HumanResources.Messages.Replies;
using HumanResources.UI.ViewModels;
using HumanResources.Validation.Contracts;
using NServiceBus;

namespace HumanResources.UI.Controllers
{
    public class HolidayController : AsyncController
    {
        private readonly IBus _bus;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHolidayValidator _holidayValidator;

        public HolidayController(
            IBus bus, 
            IEmployeeRepository employeeRepository,
            IHolidayValidator holidayValidator)
        {
            _bus = bus;
            _employeeRepository = employeeRepository;
            _holidayValidator = holidayValidator;
        }

        public ActionResult Index(Guid employeeId)
        {
            Employee employee;

            using (var transactionScope = new TransactionScope())
            {
                employee = _employeeRepository.GetById(employeeId);
                transactionScope.Complete();
            }

            var viewModel = new IndexHolidaysViewModel
                                {
                                    EmployeeId = employee.Id.Value,
                                    EmployeeFullName = employee.FullName,
                                    Records = employee.Holidays.Select(holiday => new IndexHolidaysRecordViewModel
                                                                                      {
                                                                                          Id = holiday.Id.Value,
                                                                                          Start = holiday.Start,
                                                                                          End = holiday.End,
                                                                                          Description =
                                                                                              holiday.Description
                                                                                      }).ToList()
                                };

            return View(viewModel);
        }

        public ActionResult Book(Guid employeeId)
        {
            Employee employee;

            using (var transactionScope = new TransactionScope())
            {
                employee = _employeeRepository.GetById(employeeId);
                transactionScope.Complete();
            }

            var viewModel = new BookHolidayViewModel
                                {
                                    Id = Guid.NewGuid(),
                                    EmployeeId = employeeId,
                                    EmployeeFullName = employee.FullName
                                };

            return View(viewModel);
        }

        [HttpPost]
        public void BookAsync(BookHolidayViewModel viewModel)
        {
            AsyncManager.Parameters["viewModel"] = viewModel;

            var validationMessages = _holidayValidator.ValidateBook(
                viewModel.EmployeeId,
                viewModel.Start,
                viewModel.End,
                viewModel.Description);

            if(validationMessages.Any())
            {
                validationMessages.ForEach(message => ModelState.AddModelError("", message.Text));
                return;
            }

            var bookHolidayCommand = new BookHoliday
                                         {
                                             Id = viewModel.Id,
                                             EmployeeId = viewModel.EmployeeId,
                                             Start = viewModel.Start,
                                             End = viewModel.End,
                                             Description = viewModel.Description
                                         };

            _bus.Send(bookHolidayCommand).Register<ReturnCode>(returnCode => AsyncManager.Parameters["returnCode"] = returnCode);
        }

        public ActionResult BookCompleted(BookHolidayViewModel viewModel, ReturnCode returnCode)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }

            return RedirectToAction("Index", new { viewModel.EmployeeId });
        }
    }
}
