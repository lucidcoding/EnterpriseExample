using ClientServices.Data.Core;
using ClientServices.UI.HumanResources.WCF;
using NServiceBus;
using StructureMap.Configuration.DSL;

namespace ClientServices.UI.Core
{
    public class UiRegistry : Registry
    {
        public UiRegistry()
        {
            Configure(x =>
            {
                For<IBus>().Use(MvcApplication.Bus);
                For<IEmployeeService>().Use(new EmployeeServiceClient());
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}