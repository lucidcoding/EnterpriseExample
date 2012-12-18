using Sales.Data.Core;
using StructureMap.Configuration.DSL;

namespace Sales.WCF.Core
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