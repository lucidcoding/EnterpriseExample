using System;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Calendar.Messages.Commands;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Globals;
using HumanResources.Domain.RepositoryContracts;
using HumanResources.Messages.Commands;
using HumanResources.UI.Calendar.WCF;
using HumanResources.UI.ViewModels;
using HumanResources.Validation.Contracts;
using NServiceBus;
using CalendarReplies = Calendar.Messages.Replies;
using HumanResourcesReplies = HumanResources.Messages.Replies;

namespace HumanResources.UI.Controllers
{
    public class HolidayController : AsyncController
    {
        private readonly IBus _bus;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHolidayValidator _holidayValidator;
        private readonly IAppointmentService _appointmentService;

        public HolidayController(
            IBus bus, 
            IEmployeeRepository employeeRepository,
            IHolidayValidator holidayValidator, 
            IAppointmentService appointmentService)
        {
            _bus = bus;
            _employeeRepository = employeeRepository;
            _holidayValidator = holidayValidator;
            _appointmentService = appointmentService;
        }

        public ActionResult Index(Guid employeeId)
        {
            var appointments = _appointmentService.GetByEmployeeId(employeeId);

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
                                    Records = appointments.Select(appointment => new IndexHolidaysRecordViewModel
                                                                                     {
                                                                                         Id = employee.Holidays.FirstOrDefault(x => x.AppointmentId ==  appointment.Id) != null
                                                                                             ? employee.Holidays.Single(x => x.AppointmentId == appointment.Id).Id
                                                                                             : null,
                                                                                         AppointmentType = employee.Holidays.FirstOrDefault(x => x.AppointmentId == appointment.Id) != null
                                                                                             ? "Holiday"
                                                                                             : "Other",
                                                                                         Start = appointment.Start,
                                                                                         End = appointment.End,
                                                                                         Description = employee.Holidays.FirstOrDefault(x => x.AppointmentId ==appointment.Id) !=null
                                                                                             ? employee.Holidays.Single(x => x.AppointmentId == appointment.Id).Description
                                                                                             : null,
                                                                                     }).ToList()
                                };

            //var viewModel = new IndexHolidaysViewModel
            //                    {
            //                        EmployeeId = employee.Id.Value,
            //                        EmployeeFullName = employee.FullName,
            //                        Records = employee.Holidays.Select(holiday => new IndexHolidaysRecordViewModel
            //                                                                          {
            //                                                                              Id = holiday.Id.Value,
            //                                                                              Start =
            //                                                                                  appointments.Single(
            //                                                                                      x =>
            //                                                                                      x.Id ==
            //                                                                                      holiday.AppointmentId)
            //                                                                                  .Start,
            //                                                                              End =
            //                                                                                  appointments.Single(
            //                                                                                      x =>
            //                                                                                      x.Id ==
            //                                                                                      holiday.AppointmentId)
            //                                                                                  .End,
            //                                                                              Description =
            //                                                                                  holiday.Description
            //                                                                          }).ToList()
            //                    };

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
                                    AppointmentId = Guid.NewGuid(),
                                    EmployeeId = employeeId,
                                    EmployeeFullName = employee.FullName
                                };

            return View(viewModel);
        }

        [HttpPost]
        public void BookAsync(BookHolidayViewModel viewModel)
        {
            AsyncManager.Parameters["viewModel"] = viewModel;

            var humanResourcesValidationMessages = _holidayValidator.ValidateBook(
                viewModel.EmployeeId,
                viewModel.Start,
                viewModel.End,
                viewModel.Description);

            if(humanResourcesValidationMessages.Any())
            {
                humanResourcesValidationMessages.ForEach(message => ModelState.AddModelError("", message.Text));
                return;
            }

            var calendarValidationMessages = _appointmentService.ValidateBook(new ValidateBookRequest
                                                                        {
                                                                            EmployeeId = viewModel.EmployeeId,
                                                                            DepartmentId =
                                                                                Constants.HumanResourcessDepartmentId,
                                                                            Start = viewModel.Start,
                                                                            End = viewModel.End,
                                                                            Description = viewModel.Description
                                                                        }).ToList();

            if (calendarValidationMessages.Any())
            {
                calendarValidationMessages.ForEach(message => ModelState.AddModelError("", message.Text));
                return;
            }

            var bookHolidayCommand = new BookHoliday
                                         {
                                             Id = viewModel.Id,
                                             AppointmentId = viewModel.AppointmentId,
                                             EmployeeId = viewModel.EmployeeId,
                                             Start = viewModel.Start,
                                             End = viewModel.End,
                                             Description = viewModel.Description
                                         };

            var bookAppointmentCommand = new BookAppointment
                                             {
                                                 Id = viewModel.AppointmentId,
                                                 EmployeeId = viewModel.EmployeeId,
                                                 DepartmentId = Constants.HumanResourcessDepartmentId,
                                                 Start = viewModel.Start,
                                                 End = viewModel.End,
                                             };

            _bus.Send(bookHolidayCommand).Register<HumanResourcesReplies.ReturnCode>(returnCode => AsyncManager.Parameters["bookHolidayReturnCode"] = returnCode);
            _bus.Send(bookAppointmentCommand).Register<CalendarReplies.ReturnCode>(returnCode => AsyncManager.Parameters["calendarReturnCode"] = returnCode);
        }

        public ActionResult BookCompleted(
            BookHolidayViewModel viewModel,
            HumanResourcesReplies.ReturnCode bookHolidayReturnCode,
            CalendarReplies.ReturnCode calendarReturnCode)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }

            return RedirectToAction("Index", new { viewModel.EmployeeId });
        }
    }
}
