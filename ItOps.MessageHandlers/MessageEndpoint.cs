using ItOps.MessageHandlers.Core;
using NServiceBus;
using StructureMap;

namespace ItOps.MessageHandlers
{
    public class MessageEndpoint : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .StructureMapBuilder()
                .Log4Net()
                .XmlSerializer()
                .InMemorySubscriptionStorage()
                .UnicastBus()
                    .DoNotAutoSubscribe();

            ObjectFactory.Container.Configure(x => x.AddRegistry<MessageHandlersRegistry>());
        }
    }
}
