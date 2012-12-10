using System;
using System.Web.Mvc;
using NServiceBus;
using Sales.UI.ClientServices.WCF;
using Sales.UI.ViewModels;

namespace Sales.UI.Controllers
{
    public class DealController : AsyncController
    {
        private readonly IBus _bus;
        private readonly IServiceService _serviceService;

        public DealController(IBus bus, IServiceService serviceService)
        {
            _bus = bus;
            _serviceService = serviceService;
        }

        public ActionResult Register(Guid leadId)
        {
            var services = _serviceService.GetAll();

            var viewModel = new RegisterDealViewModel
            {
                Id = Guid.NewGuid(),
                LeadId = leadId,
                Services = new SelectList(services, "Id", "Name")
            };

            return View(viewModel);
        }

        [HttpPost]
        public void RegisterAsync(RegisterDealViewModel viewModel)
        {
            
        }
    }
}
