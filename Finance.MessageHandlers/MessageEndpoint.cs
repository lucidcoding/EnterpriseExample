using NServiceBus;
using Finance.MessageHandlers.Core;
using StructureMap;

namespace Finance.MessageHandlers
{
    public class MessageEndpoint : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .StructureMapBuilder()
                .JsonSerializer()
                .MsmqSubscriptionStorage()
                .UnicastBus()
                    .DoNotAutoSubscribe();

            ObjectFactory.Container.Configure(x => x.AddRegistry<MessageHandlersRegistry>());
        }
    }
}
