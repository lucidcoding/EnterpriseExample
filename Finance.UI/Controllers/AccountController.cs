using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Finance.Domain.Entities;
using Finance.Domain.RepositoryContracts;
using Finance.Messages.Commands;
using Finance.Messages.Replies;
using Finance.UI.ClientServices.WCF;
using Finance.UI.ViewModel;
using NServiceBus;

namespace Finance.UI.Controllers
{
    public class AccountController : AsyncController
    {
        private readonly IBus _bus;
        private readonly IAccountRepository _accountRepository;
        private readonly IClientService _clientService;

        public AccountController(
            IBus bus,
            IAccountRepository accountRepository,
            IClientService clientService)
        {
            _bus = bus;
            _accountRepository = accountRepository;
            _clientService = clientService;
        }

        public ActionResult Index()
        {
            IList<Account> accounts;

            using (var transactionScope = new TransactionScope())
            {
                accounts = _accountRepository.GetAll();
                transactionScope.Complete();
            }

            var clients = _clientService.GetByIds(accounts.Select(account => account.ClientId).ToArray());

            var viewModel = accounts.Select(account => new IndexAccountsViewModel
                                                           {
                                                               Id = account.Id.Value,
                                                               ClientReference = clients.Single(client => client.Id == account.ClientId).Reference,
                                                               ClientName = clients.Single(client => client.Id == account.ClientId).Name,
                                                               Expiry = account.Expiry,
                                                               Value = account.Value,
                                                               Status = account.Status
                                                           });

            return View(viewModel);
        }

        public void SuspendAsync(Guid accountId)
        {
            var command = new SuspendAccount {Id = accountId};
            _bus.Send(command).Register<ReturnCode>(status => AsyncManager.Parameters["returnCode"] = status);
        }

        public ActionResult SuspendCompleted(ReturnCode returnCode)
        {
            return RedirectToAction("Index");
        }
    }
}
