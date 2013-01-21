using Calendar.Data.Core;
using Calendar.Validation.Core;
using StructureMap.Configuration.DSL;

namespace Calendar.WCF.Core
{
    public class WcfRegistry : Registry
    {
        public WcfRegistry()
        {
            Configure(x =>
                          {
                              x.ImportRegistry(typeof(DataRegistry));
                              x.ImportRegistry(typeof(ValidationRegistry));
                          });
        }
    }
}