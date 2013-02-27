using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Finance.Domain.Entities;
using Finance.Domain.RepositoryContracts;
using Finance.Messages.Commands;
using Finance.Messages.Replies;
using Finance.UI.ViewModel;
using ItOps.Messages.Commands;
using NServiceBus;

namespace Finance.UI.Controllers
{
    public class InstallmentController : AsyncController
    {
        private readonly IBus _bus;
        private readonly IInstallmentRepository _installmentRepository;

        public InstallmentController(
            IBus bus,
            IInstallmentRepository installmentRepository)
        {
            _bus = bus;
            _installmentRepository = installmentRepository;
        }

        public ActionResult Index(Guid accountId)
        {
            IList<Installment> installments;

            using (var transactionScope = new TransactionScope())
            {
                installments = _installmentRepository.GetByAccountId(accountId);
                transactionScope.Complete();
            }

            var viewModel = new IndexInstallmentsViewModel
                                {
                                    AccountId = accountId,
                                    Records = installments.Select(installment => new IndexInstallmentsRecordViewModel
                                                                                     {
                                                                                         Id = installment.Id.Value,
                                                                                         Amount = installment.Amount,
                                                                                         DueDate = installment.DueDate,
                                                                                         Status = installment.Status
                                                                                     }).ToList()
                                };

            return View(viewModel);
        }

        public void SendInvoiceAsync(Guid installmentId, Guid accountId)
        {
            AsyncManager.Parameters["accountId"] = accountId;
            var command = new SendInvoice { InstallmentId = installmentId };
            _bus.Send(command).Register<ReturnCode>(status => AsyncManager.Parameters["returnCode"] = status);
        }

        public ActionResult SendInvoiceCompleted(Guid accountId, ReturnCode returnCode)
        {
            return View();
        }

        public void MarkAsPaidAsync(Guid installmentId, Guid accountId)
        {
            AsyncManager.Parameters["accountId"] = accountId;
            var command = new MarkInstallmentAsPaid { Id = installmentId };
            _bus.Send(command).Register<ReturnCode>(status => AsyncManager.Parameters["returnCode"] = status);
        }

        public ActionResult MarkAsPaidCompleted(Guid accountId, ReturnCode returnCode)
        {
            return RedirectToAction("Index", new { accountId });
        }
    }
}
