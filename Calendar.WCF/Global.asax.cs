using System;
using System.Web;
using Calendar.WCF.Core;
using StructureMap;
using NServiceBus;

namespace Calendar.WCF
{
    public class Global : HttpApplication
    {
        public static IBus Bus { get; private set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            Bus = Configure.With()
                .StructureMapBuilder()
                //For WCF?
                .JsonSerializer()
                .Log4Net()
                .MsmqTransport()
                    .IsTransactional(false)
                    .PurgeOnStartup(false)
                .DoNotCreateQueues()
                .UnicastBus()
                    .ImpersonateSender(false)
                .CreateBus()
                .Start(() => Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());

            ObjectFactory.Container.Configure(x => x.AddRegistry<WcfRegistry>());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}