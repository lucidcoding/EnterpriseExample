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
using ClientServices.UI.Sales.WCF;
using ClientServices.UI.ViewModels;
using NServiceBus;

namespace ClientServices.UI.Controllers
{
    public class ClientController : AsyncController
    {
        private readonly IBus _bus;
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeeService _employeeService;
        private readonly ILeadService _leadService;

        public ClientController(
            IBus bus,
            IClientRepository clientRepository,
            IEmployeeService employeeService,
            ILeadService leadService)
        {
            _bus = bus;
            _clientRepository = clientRepository;
            _employeeService = employeeService;
            _leadService = leadService;
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

            var initializedClientsDetails = _leadService.GetByIds(initializedClients
                .Where(client => client.Id.HasValue)
                .Select(client => client.Id.Value)
                .ToArray());

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
                                                                                    Name = initializedClientsDetails.Single(x => x.Id == client.Id).Name,
                                                                                    Address1 = initializedClientsDetails.Single(x => x.Id == client.Id).Address1,
                                                                                    Address2 = initializedClientsDetails.Single(x => x.Id == client.Id).Address2,
                                                                                    Address3 = initializedClientsDetails.Single(x => x.Id == client.Id).Address3,
                                                                                    PhoneNumber = initializedClientsDetails.Single(x => x.Id == client.Id).PhoneNumber
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

            var initializedClientDetails = _leadService.GetById(clientId);
            var employees = _employeeService.GetCurrentByDepartmentId(Constants.ClientServicesDepartmentId);

            var viewModel = new ActivateClientViewModel
                                {
                                    Id = initializedClient.Id.Value,
                                    Name = initializedClientDetails.Name,
                                    Address1 = initializedClientDetails.Address1,
                                    Address2 = initializedClientDetails.Address2,
                                    Address3 = initializedClientDetails.Address3,
                                    PhoneNumber = initializedClientDetails.PhoneNumber,
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
