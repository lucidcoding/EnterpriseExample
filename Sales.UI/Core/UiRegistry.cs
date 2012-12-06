using NServiceBus;
using Sales.Application.Core;
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
                x.ImportRegistry(typeof(ApplicationRegistry));
            });
        }
    }
}