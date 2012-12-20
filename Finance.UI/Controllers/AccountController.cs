using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Finance.Domain.Entities;
using Finance.Domain.RepositoryContracts;
using Finance.UI.ClientServices.WCF;
using Finance.UI.ViewModel;

namespace Finance.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IClientService _clientService;

        public AccountController(
            IAccountRepository accountRepository,
            IClientService clientService)
        {
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
                                                               Client = clients.Single(client => client.Id == account.ClientId).Name,
                                                               Expiry = account.Expiry,
                                                               Value = account.Value,
                                                               Status = account.Status
                                                           });

            return View(viewModel);
        }
    }
}
