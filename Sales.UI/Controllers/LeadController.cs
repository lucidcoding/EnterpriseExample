﻿using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Sales.Domain.Entities;
using Sales.Domain.RepositoryContracts;
using Sales.UI.HumanResources.WCF;
using Sales.UI.ViewModels;

namespace Sales.UI.Controllers
{
    public class LeadController : Controller
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IEmployeeService _employeeService;

        public LeadController(ILeadRepository leadRepository, IEmployeeService employeeService)
        {
            _leadRepository = leadRepository;
            _employeeService = employeeService;
        }

        public ActionResult Index()
        {
            IList<Lead> leads;

            using (var transactionScope = new TransactionScope())
            {
                leads = _leadRepository.GetUnsigned();
                transactionScope.Complete();
            }

            var consultants = _employeeService
                .GetByIds(leads
                              .Where(x => x.AssignedToConsultantId.HasValue)
                              .Select(x => x.AssignedToConsultantId.Value)
                              .ToArray());

            var viewModel = (from lead in leads
                             join jConsultant in consultants
                                 on lead.AssignedToConsultantId equals jConsultant.Id
                                 into jConsultants
                             from consultant in jConsultants.DefaultIfEmpty()
                             select new IndexLeadsRecordViewModel
                                        {
                                            Id = lead.Id.Value,
                                            Name = lead.Name,
                                            Address1 = lead.Address1,
                                            Address2 = lead.Address2,
                                            Address3 = lead.Address3,
                                            AssignedToConsultantName = consultant != null ? consultant.FullName : null,
                                        }).ToList();

            //Alternative way of joining entities:
            //var viewModel = leads.Select(x => new IndexLeadsRecordViewModel
            //                                      {
            //                                          Id = x.Id.Value,
            //                                          Name = x.Name,
            //                                          Address1 = x.Address1,
            //                                          Address2 = x.Address2,
            //                                          Address3 = x.Address3,
            //                                          AssignedToConsultantName =
            //                                              x.AssignedToConsultantId.HasValue
            //                                                  ? consultants.Single(
            //                                                      y => y.Id == x.AssignedToConsultantId.Value).FullName
            //                                                  : null
            //                                      }).ToList();

            return View(viewModel);
        }

        public ActionResult Add()
        {
            return View("ComingSoon");
        }
    }
}
