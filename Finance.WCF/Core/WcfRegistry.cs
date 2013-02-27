using StructureMap.Configuration.DSL;
using Finance.Data.Core;

namespace Finance.WCF.Core
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