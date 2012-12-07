using HumanResources.Data.Core;
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
                              For<IEmployeeService>().Use<EmployeeService>();
                              x.ImportRegistry(typeof (DataRegistry));
                          });
        }
    }
}