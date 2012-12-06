using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales.Application.Contracts;
using Sales.UI.HumanResources.WCF;
using Sales.UI.ViewModels;

namespace Sales.UI.Controllers
{
    public class LeadController : Controller
    {
        private readonly ILeadService _leadService;
        private readonly IEmployeeService _employeeService;

        public LeadController(ILeadService leadService, IEmployeeService employeeService)
        {
            _leadService = leadService;
            _employeeService = employeeService;
        }

        public ActionResult Index()
        {
            var leads = _leadService.GetUnsigned();

            var consultants = _employeeService
                .GetByIds(leads
                              .Where(x => x.AssignedToConsultantId.HasValue)
                              .Select(x => x.AssignedToConsultantId.Value)
                              .ToArray());

            var viewModel = leads.Select(x => new IndexLeadsViewModel
                                                  {
                                                      Id = x.Id.Value,
                                                      Name = x.Name,
                                                      Address1 = x.Address1,
                                                      Address2 = x.Address2,
                                                      Address3 = x.Address3,
                                                      AssignedToConsultantName =
                                                          x.AssignedToConsultantId.HasValue
                                                              ? consultants.Single(
                                                                  y => y.Id == x.AssignedToConsultantId.Value).FullName
                                                              : null
                                                  }).ToList();

            return View(viewModel);
        }

    }
}
