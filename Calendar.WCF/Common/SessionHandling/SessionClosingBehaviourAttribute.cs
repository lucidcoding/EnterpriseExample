using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Calendar.WCF.Common.SessionHandling
{
    public class SessionClosingBehaviourAttribute : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        { }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase chanDispBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = chanDispBase as ChannelDispatcher;
                if (channelDispatcher == null)
                    continue;

                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                    SessionClosingDispatchMessageInspector inspector = new SessionClosingDispatchMessageInspector();
                    endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        { }
    }
}
