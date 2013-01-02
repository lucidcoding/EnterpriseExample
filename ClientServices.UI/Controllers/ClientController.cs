using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using ClientServices.Domain.Entities;
using ClientServices.Domain.Globals;
using ClientServices.Domain.RepositoryContracts;
using ClientServices.Messages.Commands;
using ClientServices.Messages.Replies;
using ClientServices.UI.HumanResources.WCF;
using ClientServices.UI.ViewModels;
using NServiceBus;

namespace ClientServices.UI.Controllers
{
    public class ClientController : AsyncController
    {
        private readonly IBus _bus;
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeeService _employeeService;

        public ClientController(
            IBus bus,
            IClientRepository clientRepository,
            IEmployeeService employeeService)
        {
            _bus = bus;
            _clientRepository = clientRepository;
            _employeeService = employeeService;
        }

        public ActionResult Index()
        {
            IList<Client> initializedClients;
            IList<Client> activeClients;

            using (var transactionScope = new TransactionScope())
            {
                initializedClients = _clientRepository.GetInitialized();
                activeClients = _clientRepository.GetActive();
                transactionScope.Complete();
            }

            var employees = _employeeService.GetByIds(activeClients
                                                          .Where(client => client.LiasonEmployeeId.HasValue)
                                                          .Select(client => client.LiasonEmployeeId.Value)
                                                          .ToArray());

            var viewModel = new IndexClientsViewModel
                                {
                                    InitializedClients =
                                        initializedClients.Select(client => new IndexClientsRecordViewModel
                                                                                {
                                                                                    Id = client.Id.Value,
                                                                                    Name = client.Name,
                                                                                    Address1 = client.Address1,
                                                                                    Address2 = client.Address2,
                                                                                    Address3 = client.Address3,
                                                                                    PhoneNumber = client.PhoneNumber
                                                                                }).ToList(),
                                    ActiveClients = activeClients.Select(client => new IndexClientsRecordViewModel
                                                                                       {
                                                                                           Id = client.Id.Value,
                                                                                           Name = client.Name,
                                                                                           Reference = client.Reference,
                                                                                           Address1 = client.Address1,
                                                                                           Address2 = client.Address2,
                                                                                           Address3 = client.Address3,
                                                                                           PhoneNumber = client.PhoneNumber,
                                                                                           Liason = client.LiasonEmployeeId.HasValue
                                                                                                   ? employees.Single(employee => employee.Id.Value == client.LiasonEmployeeId.Value).FullName
                                                                                                   : null
                                                                                       }).ToList(),
                                };

            return View(viewModel);
        }

        public ActionResult Activate(Guid clientId)
        {
            Client initializedClient;

            using (var transactionScope = new TransactionScope())
            {
                initializedClient = _clientRepository.GetById(clientId);
                transactionScope.Complete();
            }

            var employees = _employeeService.GetCurrentByDepartmentId(Constants.ClientServicesDepartmentId);

            var viewModel = new ActivateClientViewModel
                                {
                                    Id = initializedClient.Id.Value,
                                    Name = initializedClient.Name,
                                    Address1 = initializedClient.Address1,
                                    Address2 = initializedClient.Address2,
                                    Address3 = initializedClient.Address3,
                                    PhoneNumber = initializedClient.PhoneNumber,
                                    Employees = new SelectList(employees, "Id", "FullName")
                                };

            return View(viewModel);
        }

        [HttpPost]
        public void ActivateAsync(ActivateClientViewModel viewModel)
        {
            var command = new ActivateClient
                              {
                                  Id = viewModel.Id,
                                  Name = viewModel.Name,
                                  Reference = viewModel.Reference,
                                  Address1 = viewModel.Address1,
                                  Address2 = viewModel.Address2,
                                  Address3 = viewModel.Address3,
                                  PhoneNumber = viewModel.PhoneNumber,
                                  LiasonEmployeeId = viewModel.LiasonEmployeeId
                              };

            _bus.Send(command).Register<ReturnCode>(status =>
                                                    {
                                                        AsyncManager.Parameters["returnCode"] = status;
                                                    });
        }

        public ActionResult ActivateCompleted()
        {
            return RedirectToAction("Index");
        }
    }
}
