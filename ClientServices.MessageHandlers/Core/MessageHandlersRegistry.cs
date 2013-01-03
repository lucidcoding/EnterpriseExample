using ClientServices.Data.Core;
using ClientServices.Domain.RepositoryContracts;
using StructureMap.Configuration.DSL;

namespace ClientServices.MessageHandlers.Core
{
    public class MessageHandlersRegistry : Registry
    {
        public MessageHandlersRegistry()
        {
            Configure(x =>
                          {
                              x.ImportRegistry(typeof (DataRegistry));
                              SetAllProperties(y => y.OfType<IClientRepository>());
                              SetAllProperties(y => y.OfType<IServiceRepository>());
                          });
        }
    }
}
