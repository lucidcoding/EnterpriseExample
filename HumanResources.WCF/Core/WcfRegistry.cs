using HumanResources.Application.Core;
using NServiceBus;
using StructureMap.Configuration.DSL;

namespace HumanResources.WCF.Core
{
    public class WcfRegistry : Registry
    {
        public WcfRegistry()
        {
            Configure(x =>
                          {
                              For<IBus>().Use(Global.Bus);
                              x.ImportRegistry(typeof (ApplicationRegistry));
                          });
        }
    }
}