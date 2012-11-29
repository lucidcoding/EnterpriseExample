using HumanResources.Application.Core;
using HumanResources.UI.Calendar.WCF;
using NServiceBus;
using StructureMap.Configuration.DSL;

namespace HumanResources.UI.Core
{
    public class UiRegistry : Registry
    {
        public UiRegistry()
        {
            Configure(x =>
            {
                For<IBus>().Use(MvcApplication.Bus);
                For<IBookingService>().Use(new BookingServiceClient());
                x.ImportRegistry(typeof(ApplicationRegistry));
            });
        }
    }
}