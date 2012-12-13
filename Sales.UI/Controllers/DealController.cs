using System;
using System.Linq;
using System.Web.Mvc;
using NServiceBus;
using Sales.Messages.Commands;
using Sales.Messages.Replies;
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

        //TODO: deal needs to lod start and end date of deal and send it to CS along with serviceIds.
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
            var command = new RegisterDeal
                              {

                                  Id = viewModel.Id,
                                  LeadId = viewModel.LeadId,
                                  ServiceIds = viewModel.ServiceIds,
                                  Value = viewModel.Value
                              };

            AsyncManager.OutstandingOperations.Increment();

            _bus.Send(command).Register<ReturnCode>(status =>
                                                        {
                                                            AsyncManager.Parameters["returnCode"] = status;
                                                            AsyncManager.Parameters["leadId"] =
                                                                viewModel.LeadId;
                                                            AsyncManager.OutstandingOperations.Decrement();
                                                        });
        }

        public ActionResult RegisterCompleted(ReturnCode returnCode, Guid leadId)
        {
            return RedirectToAction("Index", new { leadId });
        }
    }
}
