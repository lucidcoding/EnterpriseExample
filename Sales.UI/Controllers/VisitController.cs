using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using NServiceBus;
using Sales.Domain.Entities;
using Sales.Domain.RepositoryContracts;
using Sales.Messages.Commands;
using Sales.Messages.Replies;
using Sales.UI.ViewModels;

namespace Sales.UI.Controllers
{
    public class VisitController : AsyncController
    {
        private readonly IBus _bus;
        private readonly ILeadRepository _leadRepository;
        private readonly IVisitRepository _visitRepository;

        public VisitController(
            IBus bus, 
            ILeadRepository leadRepository,
            IVisitRepository visitRepository)
        {
            _bus = bus;
            _leadRepository = leadRepository;
            _visitRepository = visitRepository;
        }

        public ActionResult Index(Guid leadId)
        {
            Lead lead;
            IList<Visit> visits;

            using (var transactionScope = new TransactionScope())
            {
                lead = _leadRepository.GetById(leadId);
                visits = _visitRepository.GetByLeadId(leadId);
                transactionScope.Complete();
            }

            var viewModel = new IndexVisitsViewModel
                                {
                                    LeadId = leadId,
                                    LeadName = lead.Name,
                                    Records = visits.Select(visit => new IndexVisitsRecordViewModel
                                                                         {
                                                                             Id = visit.Id.Value,
                                                                             Start = visit.Start,
                                                                             End = visit.End,
                                                                         }).ToList()
                                };
                

            return View(viewModel);
        }

        public ActionResult Log(Guid leadId)
        {
            var viewModel = new LogVisitViewModel
                                {
                                    Id = Guid.NewGuid(),
                                    LeadId = leadId
                                };

            return View(viewModel);
        }

        [HttpPost]
        public void LogAsync(LogVisitViewModel viewModel)
        {
            var command = new LogVisit
                              {
                                  Id = viewModel.Id,
                                  LeadId = viewModel.LeadId,
                                  Start = viewModel.Start,
                                  End = viewModel.End
                              };

            AsyncManager.OutstandingOperations.Increment();

            _bus.Send(command).Register<ReturnCode>(status =>
                                                        {
                                                            AsyncManager.Parameters["returnCode"] = status;
                                                            AsyncManager.Parameters["resultedInDeal"] =
                                                                viewModel.ResultedInDeal;
                                                            AsyncManager.Parameters["leadId"] =
                                                                viewModel.LeadId;
                                                            AsyncManager.OutstandingOperations.Decrement();
                                                        });
        }

        public ActionResult LogCompleted(ReturnCode returnCode, bool resultedInDeal, Guid leadId)
        {
            if (resultedInDeal)
            {
                return RedirectToAction("Register", "Deal", new { leadId });
            }

            return RedirectToAction("Index", new { leadId });
        }
    }
}
