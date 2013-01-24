using ClientServices.MessageHandlers.Core;
using NServiceBus;
using StructureMap;

namespace ClientServices.MessageHandlers
{
    public class MessageEndpoint : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            ObjectFactory.Container.Configure(x => x.AddRegistry<MessageHandlersRegistry>());

            Configure.With()
                .StructureMapBuilder()
                .XmlSerializer()
                .InMemorySubscriptionStorage() 
                .Sagas()  
                .InMemorySagaPersister()
                .UnicastBus()
                    .DoNotAutoSubscribe();
        }
    }
}
