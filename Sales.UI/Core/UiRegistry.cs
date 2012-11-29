using NServiceBus;
using Sales.Application.Core;
using Sales.UI.Calendar.WCF;
using Sales.UI.HumanResources.WCF;
using StructureMap.Configuration.DSL;

namespace Sales.UI.Core
{
    public class UiRegistry : Registry
    {
        public UiRegistry()
        {
            Configure(x =>
            {
                For<IBus>().Use(MvcApplication.Bus);
                For<IEmployeeService>().Use(new EmployeeServiceClient());
                For<IBookingService>().Use(new BookingServiceClient());
                x.ImportRegistry(typeof(ApplicationRegistry));
            });
        }
    }
}