using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales.UI.ViewModels;

namespace Sales.UI.Controllers
{
    public class VisitController : Controller
    {
        public VisitController()
        {
            
        }

        public ActionResult Log(Guid leadId)
        {
            var viewModel = new LogVisitViewModel
                                {
                                    Id = Guid.NewGuid(),
                                    LeadId = leadId
                                };

            return View(viewModel);
        }

        [HttpPost]
        public void LogAsync(LogVisitViewModel viewModel)
        {
  
        }
    }
}
