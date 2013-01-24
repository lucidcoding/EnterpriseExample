using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Calendar.Messages.Commands;
using NServiceBus;
using Sales.Domain.Entities;
using Sales.Domain.Globals;
using Sales.Domain.RepositoryContracts;
using Sales.Messages.Commands;
using Sales.UI.Calendar.WCF;
using Sales.UI.HumanResources.WCF;
using Sales.UI.ViewModels;
using CalendarReplies = Calendar.Messages.Replies;
using SalesReplies = Sales.Messages.Replies;

namespace Sales.UI.Controllers
{
    public class VisitController : AsyncController
    {
        private readonly IBus _bus;
        private readonly ILeadRepository _leadRepository;
        private readonly IVisitRepository _visitRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IAppointmentService _appointmentService;

        public VisitController(
            IBus bus, 
            ILeadRepository leadRepository,
            IVisitRepository visitRepository,
            IEmployeeService employeeService,
            IAppointmentService appointmentService)
        {
            _bus = bus;
            _leadRepository = leadRepository;
            _visitRepository = visitRepository;
            _employeeService = employeeService;
            _appointmentService = appointmentService;
        }

        public ActionResult Index(Guid leadId)
        {
            Lead lead;
            IList<Visit> bookedVisits;
            IList<Visit> completedVisits;

            using (var transactionScope = new TransactionScope())
            {
                lead = _leadRepository.GetById(leadId);
                bookedVisits = _visitRepository.GetBookedByLeadId(leadId);
                completedVisits = _visitRepository.GetCompletedByLeadId(leadId);
                transactionScope.Complete();
            }

            var consultantIds = bookedVisits
                .Union(completedVisits)
                .Where(x => x.ConsultantId.HasValue)
                .Select(x => x.ConsultantId.Value)
                .ToArray();

            var consultants = _employeeService.GetByIds(consultantIds);

            var appointmentIds = bookedVisits
                .Union(completedVisits)
                .Select(x => x.AppointmentId.Value)
                .ToArray();

            var appointments = _appointmentService.GetByIds(appointmentIds);

            var viewModel = new IndexVisitsViewModel
                                {
                                    LeadId = leadId,
                                    LeadName = lead.Name,
                                    BookedVisits = (from visit in bookedVisits
                                                    join appointment in appointments
                                                        on visit.AppointmentId equals appointment.Id.Value
                                                    join joinConsultant in consultants
                                                        on visit.ConsultantId equals joinConsultant.Id
                                                        into joinConsultants
                                                    from consultant in joinConsultants.DefaultIfEmpty()
                                                    select new IndexVisitsRecordViewModel
                                                               {
                                                                   Id = visit.Id.Value,
                                                                   Start = appointment.Start,
                                                                   End = appointment.End,
                                                                   ConsultantName = consultant != null ? consultant.FullName : null
                                                               }).ToList(),
                                    CompletedVisits = (from visit in completedVisits
                                                       join appointment in appointments
                                                           on visit.AppointmentId equals appointment.Id.Value
                                                       join joinConsultant in consultants
                                                           on visit.ConsultantId equals joinConsultant.Id
                                                           into joinConsultants
                                                       from consultant in joinConsultants.DefaultIfEmpty()
                                                       select new IndexVisitsRecordViewModel
                                                                  {
                                                                      Id = visit.Id.Value,
                                                                      Start = appointment.Start,
                                                                      End = appointment.End,
                                                                      ConsultantName = consultant != null ? consultant.FullName : null
                                                                  }).ToList()
                                };

            //var viewModel2 = new IndexVisitsViewModel
            //                    {
            //                        LeadId = leadId,
            //                        LeadName = lead.Name,
            //                        BookedVisits = bookedVisits.Select(visit => new IndexVisitsRecordViewModel
            //                                                                        {
            //                                                                            Id = visit.Id.Value,
            //                                                                            Start =
            //                                                                                appointments.Single(
            //                                                                                    x =>
            //                                                                                    x.Id ==
            //                                                                                    visit.AppointmentId).Start,
            //                                                                            End =
            //                                                                                appointments.Single(
            //                                                                                    x =>
            //                                                                                    x.Id ==
            //                                                                                    visit.AppointmentId).End,
            //                                                                            ConsultantName =
            //                                                                                visit.ConsultantId.HasValue
            //                                                                                    ? consultants.Single(
            //                                                                                        x =>
            //                                                                                        x.Id ==
            //                                                                                        visit.
            //                                                                                            ConsultantId)
            //                                                                                          .
            //                                                                                          FullName
            //                                                                                    : null
            //                                                                        }).ToList(),
            //                        CompletedVisits = completedVisits.Select(visit => new IndexVisitsRecordViewModel
            //                                                                              {
            //                                                                                  Id = visit.Id.Value,
            //                                                                                  Start =
            //                                                                                      appointments.Single(
            //                                                                                          x =>
            //                                                                                          x.Id ==
            //                                                                                          visit.
            //                                                                                              AppointmentId).Start,
            //                                                                                  End =
            //                                                                                      appointments.Single(
            //                                                                                          x =>
            //                                                                                          x.Id ==
            //                                                                                          visit.
            //                                                                                              AppointmentId).End,
            //                                                                                  ConsultantName =
            //                                                                                      visit.ConsultantId.
            //                                                                                          HasValue
            //                                                                                          ? consultants.
            //                                                                                                Single(
            //                                                                                                    x =>
            //                                                                                                    x.Id ==
            //                                                                                                    visit.
            //                                                                                                        ConsultantId)
            //                                                                                                .FullName
            //                                                                                          : null
            //                                                                              }).ToList(),
            //                    };
                
            return View(viewModel);
        }

        public ActionResult Book(Guid leadId)
        {
            var consultants = _employeeService.GetCurrentByDepartmentId(Constants.SalesDepartmentId);

            var viewModel = new BookVisitViewModel
                                {
                                    Id = Guid.NewGuid(),
                                    AppointmentId = Guid.NewGuid(),
                                    LeadId = leadId,
                                    Consultants = new SelectList(consultants, "Id", "FullName")
                                };

            return View(viewModel);
        }

        [HttpPost]
        public void BookAsync(BookVisitViewModel viewModel)
        {
            AsyncManager.Parameters["viewModel"] = viewModel;

            var calendarValidationMessages = _appointmentService.ValidateBook(new ValidateBookRequest
            {
                EmployeeId = viewModel.ConsultantId.Value,
                DepartmentId =
                    Constants.SalesDepartmentId,
                Start = viewModel.Start,
                End = viewModel.End
            }).ToList();

            if (calendarValidationMessages.Any())
            {
                calendarValidationMessages.ForEach(message => ModelState.AddModelError("", message.Text));
                return;
            }

            var bookVisitCommand = new BookVisit
                              {
                                  Id = viewModel.Id,
                                  AppointmentId = viewModel.AppointmentId,
                                  LeadId = viewModel.LeadId,
                                  Start = viewModel.Start,
                                  End = viewModel.End,
                                  ConsultantId = viewModel.ConsultantId
                              };

            var bookAppointmentCommand = new BookAppointment
                                             {
                                                 Id = viewModel.AppointmentId,
                                                 EmployeeId = viewModel.ConsultantId.Value,
                                                 DepartmentId = Constants.SalesDepartmentId,
                                                 Start = viewModel.Start,
                                                 End = viewModel.End,
                                             };

            _bus.Send(bookVisitCommand).Register<SalesReplies.ReturnCode>(returnCode => AsyncManager.Parameters["salesReturnCode"] = returnCode);
            _bus.Send(bookAppointmentCommand).Register<CalendarReplies.ReturnCode>(returnCode => AsyncManager.Parameters["calendarReturnCode"] = returnCode);
        }

        public ActionResult BookCompleted(
            BookVisitViewModel viewModel,
            SalesReplies.ReturnCode salesReturnCode,
            CalendarReplies.ReturnCode calendarReturnCode)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            return RedirectToAction("Index", new { viewModel.LeadId });
        }

        public void CompleteAsync(Guid visitId, Guid leadId)
        {
            AsyncManager.Parameters["leadId"] = leadId;
            var command = new CompleteVisit {Id = visitId};

            _bus.Send(command).Register<SalesReplies.ReturnCode>(status => AsyncManager.Parameters["returnCode"] = status);
        }

        public ActionResult CompleteCompleted(SalesReplies.ReturnCode returnCode, Guid leadId)
        {
            return RedirectToAction("Index", new { leadId });
        }
    }
}
