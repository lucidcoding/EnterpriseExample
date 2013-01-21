using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using NServiceBus;
using Sales.Domain.Entities;
using Sales.Domain.Globals;
using Sales.Domain.RepositoryContracts;
using Sales.Messages.Commands;
using Sales.Messages.Replies;
using Sales.UI.Calendar.WCF;
using Sales.UI.HumanResources.WCF;
using Sales.UI.ViewModels;

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
                                    BookedVisits = bookedVisits.Select(visit => new IndexVisitsRecordViewModel
                                                                                    {
                                                                                        Id = visit.Id.Value,
                                                                                        Start =
                                                                                            appointments.Single(
                                                                                                x =>
                                                                                                x.Id ==
                                                                                                visit.AppointmentId).Start,
                                                                                        End =
                                                                                            appointments.Single(
                                                                                                x =>
                                                                                                x.Id ==
                                                                                                visit.AppointmentId).End,
                                                                                        ConsultantName =
                                                                                            visit.ConsultantId.HasValue
                                                                                                ? consultants.Single(
                                                                                                    x =>
                                                                                                    x.Id ==
                                                                                                    visit.
                                                                                                        ConsultantId)
                                                                                                      .
                                                                                                      FullName
                                                                                                : null
                                                                                    }).ToList(),
                                    CompletedVisits = completedVisits.Select(visit => new IndexVisitsRecordViewModel
                                                                                          {
                                                                                              Id = visit.Id.Value,
                                                                                              Start =
                                                                                                  appointments.Single(
                                                                                                      x =>
                                                                                                      x.Id ==
                                                                                                      visit.
                                                                                                          AppointmentId).Start,
                                                                                              End =
                                                                                                  appointments.Single(
                                                                                                      x =>
                                                                                                      x.Id ==
                                                                                                      visit.
                                                                                                          AppointmentId).End,
                                                                                              ConsultantName =
                                                                                                  visit.ConsultantId.
                                                                                                      HasValue
                                                                                                      ? consultants.
                                                                                                            Single(
                                                                                                                x =>
                                                                                                                x.Id ==
                                                                                                                visit.
                                                                                                                    ConsultantId)
                                                                                                            .FullName
                                                                                                      : null
                                                                                          }).ToList(),
                                };
                
            return View(viewModel);
        }

        public ActionResult Book(Guid leadId)
        {
            var consultants = _employeeService.GetCurrentByDepartmentId(Constants.SalesDepartmentId);

            var viewModel = new BookVisitViewModel
                                {
                                    Id = Guid.NewGuid(),
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

            var command = new BookVisit
                              {
                                  Id = viewModel.Id,
                                  LeadId = viewModel.LeadId,
                                  Start = viewModel.Start,
                                  End = viewModel.End,
                                  ConsultantId = viewModel.ConsultantId
                              };

            _bus.Send(command).Register<ReturnCode>(status =>
                                                        {
                                                            AsyncManager.Parameters["returnCode"] = status;
                                                        });
        }

        public ActionResult BookCompleted(
            BookVisitViewModel viewModel, 
            ReturnCode returnCode)
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

            _bus.Send(command).Register<ReturnCode>(status =>
                                                        {
                                                            AsyncManager.Parameters["returnCode"] = status;
                                                        });
        }

        public ActionResult CompleteCompleted(ReturnCode returnCode, Guid leadId)
        {
            return RedirectToAction("Index", new { leadId });
        }
    }
}
