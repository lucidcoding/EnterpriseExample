using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using ClientServices.Domain.Entities;
using ClientServices.Domain.RepositoryContracts;
using ClientServices.Messages.Commands;
using ClientServices.Messages.Replies;
using ClientServices.UI.ViewModels;
using NServiceBus;

namespace ClientServices.UI.Controllers
{
    public class AgreementController : AsyncController
    {
        private readonly IBus _bus;
        private readonly IAgreementRepository _agreementRepository;

        public AgreementController(
            IBus bus,
            IAgreementRepository agreementRepository)
        {
            _bus = bus;
            _agreementRepository = agreementRepository;
        }

        public ActionResult Index(Guid clientId)
        {
            IList<Agreement> agreements;

            using(var transactionScope = new TransactionScope())
            {
                agreements = _agreementRepository.GetByClientId(clientId);
                transactionScope.Complete();
            }

            var viewModel = new IndexAgreementsViewModel
                                {
                                    ClientId = clientId,
                                    Records = agreements.Select(agreement => new IndexAgreementsRecordViewModel
                                                                                 {
                                                                                     Id = agreement.Id.Value,
                                                                                     Commencement = agreement.Commencement,
                                                                                     Expiry = agreement.Expiry,
                                                                                     Status = agreement.Status
                                                                                 }).ToList()
                                };

            return View(viewModel);
        }

        public void CancelAsync(Guid agreementId, Guid clientId)
        {
            AsyncManager.Parameters["clientId"] = clientId;
            var command = new CancelAgreement { Id = agreementId };
            _bus.Send(command).Register<ReturnCode>(status => AsyncManager.Parameters["returnCode"] = status);
        }

        public ActionResult CancelCompleted(Guid clientId, ReturnCode returnCode)
        {
            return RedirectToAction("Index", new { clientId });
        }
    }
}
