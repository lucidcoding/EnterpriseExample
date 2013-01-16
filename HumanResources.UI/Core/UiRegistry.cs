using HumanResources.Data.Core;
using HumanResources.Validation.Core;
using StructureMap.Configuration.DSL;

namespace HumanResources.UI.Core
{
    public class UiRegistry : Registry
    {
        public UiRegistry()
        {
            Configure(x =>
            {
                x.ImportRegistry(typeof(ValidationRegistry));
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}