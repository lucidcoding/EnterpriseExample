using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Calendar.Data.Common;
using StructureMap;

namespace Calendar.WCF.Common.SessionHandling
{
    public class SessionClosingDispatchMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var sessionProvider = ObjectFactory.GetInstance<ISessionProvider>();
            sessionProvider.CloseCurrent();
        }
    }
}
