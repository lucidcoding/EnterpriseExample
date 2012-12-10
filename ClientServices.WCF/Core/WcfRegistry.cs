using ClientServices.Data.Core;
using NServiceBus;
using StructureMap.Configuration.DSL;

namespace ClientServices.WCF.Core
{
    public class WcfRegistry : Registry
    {
        public WcfRegistry()
        {
            Configure(x =>
                          {
                              x.ImportRegistry(typeof (DataRegistry));
                          });
        }
    }
}