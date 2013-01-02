﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using ClientServices.Messages.Commands;
using NServiceBus;
using Sales.Domain.Entities;
using Sales.Domain.RepositoryContracts;
using Sales.Messages.Commands;
using Sales.UI.ClientServices.WCF;
using Sales.UI.HumanResources.WCF;
using Sales.UI.ViewModels;
using ClientServiceReplies = ClientServices.Messages.Replies;
using SalesReplies = Sales.Messages.Replies;

namespace Sales.UI.Controllers
{
    public class DealController : AsyncController
    {
        private readonly IBus _bus;
        private readonly ILeadRepository _leadRepository;
        private readonly IDealRepository _dealRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IServiceService _serviceService;

        public DealController(
            IBus bus, 
            ILeadRepository leadRepository,
            IDealRepository dealRepository,
            IEmployeeService employeeService,
            IServiceService serviceService)
        {
            _bus = bus;
            _leadRepository = leadRepository;
            _dealRepository = dealRepository;
            _employeeService = employeeService;
            _serviceService = serviceService;
        }

        public ActionResult Index(Guid leadId)
        {
            Lead lead;
            IList<Deal> deals;

            using (var transactionScope = new TransactionScope())
            {
                lead = _leadRepository.GetById(leadId);
                deals = _dealRepository.GetByLeadId(leadId);
                transactionScope.Complete();
            }

            var employees = _employeeService.GetByIds(deals.Select(deal => deal.MadeByConsultantId).ToArray());

            var viewModel = new IndexDealsViewModel
                                {
                                    LeadId = leadId,
                                    LeadName = lead.Name,
                                    Records = deals.Select(deal => new IndexDealsRecordViewModel
                                                                       {
                                                                           Id = deal.Id.Value,
                                                                           MadeByConsultant =
                                                                               employees.Single(
                                                                                   employee =>
                                                                                   employee.Id.Value ==
                                                                                   deal.MadeByConsultantId).FullName,
                                                                           Value = deal.Value
                                                                       }).ToList()
                                };

            return View(viewModel);
        }

        public ActionResult Register(Guid leadId)
        {
            var services = _serviceService
                .GetAll()
                .OrderBy(service => service.Name);

            var viewModel = new RegisterDealViewModel
            {
                Id = Guid.NewGuid(),
                LeadId = leadId,
                Services = new MultiSelectList(services, "Id", "Name")
            };

            return View(viewModel);
        }

        [HttpPost]
        public void RegisterAsync(RegisterDealViewModel viewModel)
        {
            AsyncManager.Parameters["leadId"] = viewModel.LeadId;
            var correlationId = Guid.NewGuid();

            var registerDeal = new RegisterDeal
                              {
                                  CorrelationId = correlationId,
                                  DealId = viewModel.Id,
                                  LeadId = viewModel.LeadId,
                                  Value = viewModel.Value
                              };

            var initializeClient = new InitializeClient
                                       {
                                           CorrelationId = correlationId,
                                           AgreementId = viewModel.Id,
                                           AgreementCommencement = viewModel.Commencement,
                                           AgreementExpiry = viewModel.Expiry,
                                           AgreementServiceIds = viewModel.ServiceIds,
                                           AgreementValue = viewModel.Value,
                                           ClientId = viewModel.LeadId
                                       };

            _bus.Send(registerDeal).Register<SalesReplies.ReturnCode>(status =>
                                                                          {
                                                                              AsyncManager.Parameters["registerDealReturnCode"] = status;
                                                                          });

            _bus.Send(initializeClient);
        }

        public ActionResult RegisterCompleted(
            Guid leadId,
            SalesReplies.ReturnCode registerDealReturnCode)
        {
            return RedirectToAction("Index", new { leadId });
        }
    }
}
