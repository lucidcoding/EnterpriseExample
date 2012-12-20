using Finance.Data.Core;
using Finance.UI.ClientServices.WCF;
using NServiceBus;
using StructureMap.Configuration.DSL;

namespace Finance.UI.Core
{
    public class UiRegistry : Registry
    {
        public UiRegistry()
        {
            Configure(x =>
            {
                For<IBus>().Use(MvcApplication.Bus);
                For<IClientService>().Use(new ClientServiceClient());
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}