using NServiceBus;
using Sales.Data.Core;
using Sales.UI.Calendar.WCF;
using Sales.UI.ClientServices.WCF;
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
                For<IServiceService>().Use(new ServiceServiceClient());
                For<IAppointmentService>().Use(new AppointmentServiceClient());
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}