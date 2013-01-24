using HumanResources.MessageHandlers.Core;
using NServiceBus;
using StructureMap;

namespace HumanResources.MessageHandlers
{
    public class MessageEndpoint : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .StructureMapBuilder()
                .XmlSerializer()
                .InMemorySubscriptionStorage()
                .UnicastBus()
                    .DoNotAutoSubscribe();

            ObjectFactory.Container.Configure(x => x.AddRegistry<MessageHandlersRegistry>());
        }
    }
}
