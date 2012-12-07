using HumanResources.Data.Core;
using StructureMap.Configuration.DSL;

namespace HumanResources.UI.Core
{
    public class UiRegistry : Registry
    {
        public UiRegistry()
        {
            Configure(x =>
            {
                //For<IBus>().Use(MvcApplication.Bus);
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}