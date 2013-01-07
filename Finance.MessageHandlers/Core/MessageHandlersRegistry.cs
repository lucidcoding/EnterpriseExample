using Finance.Data.Core;
using Finance.Domain.RepositoryContracts;
using StructureMap.Configuration.DSL;

namespace Finance.MessageHandlers.Core
{
    class MessageHandlersRegistry : Registry
    {
        public MessageHandlersRegistry()
        {
            Configure(x =>
                          {
                              x.ImportRegistry(typeof (DataRegistry));
                              SetAllProperties(y => y.OfType<IAccountRepository>());
                          });
        }
    }
}
