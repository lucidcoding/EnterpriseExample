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

        public VisitController(
            IBus bus, 
            ILeadRepository leadRepository,
            IVisitRepository visitRepository,
            IEmployeeService employeeService)
        {
            _bus = bus;
            _leadRepository = leadRepository;
            _visitRepository = visitRepository;
            _employeeService = employeeService;
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

            var viewModel = new IndexVisitsViewModel
                                {
                                    LeadId = leadId,
                                    LeadName = lead.Name,
                                    BookedVisits = bookedVisits.Select(visit => new IndexVisitsRecordViewModel
                                                                                    {
                                                                                        Id = visit.Id.Value,
                                                                                        Start = visit.Start,
                                                                                        End = visit.End,
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
                                                                                              Start = visit.Start,
                                                                                              End = visit.End,
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
            AsyncManager.Parameters["leadId"] = viewModel.LeadId;

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

        public ActionResult BookCompleted(ReturnCode returnCode, Guid leadId)
        {
            return RedirectToAction("Index", new { leadId });
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
