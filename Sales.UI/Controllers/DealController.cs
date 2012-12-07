using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NServiceBus;
using Sales.UI.ViewModels;

namespace Sales.UI.Controllers
{
    public class DealController : Controller
    {
        private readonly IBus _bus;

        public DealController(IBus bus)
        {
            _bus = bus;
        }

        public ActionResult Register(Guid leadId)
        {
            var viewModel = new RegisterDealViewModel
            {
                Id = Guid.NewGuid(),
                LeadId = leadId
            };

            return View(viewModel);
        }

    }
}
