using System;
using System.Web.Mvc;
using System.Web.Routing;
using ClientServices.Data.Common;
using ClientServices.UI.Common;
using ClientServices.UI.Core;
using NServiceBus;
using StructureMap;

namespace ClientServices.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IBus Bus { get; private set; }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Client", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            Bus = Configure.With()
                .StructureMapBuilder()
                .ForMvc()
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

            ObjectFactory.Container.Configure(x => x.AddRegistry<UiRegistry>());
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerActivator());
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var sessionProvider = ObjectFactory.GetInstance<ISessionProvider>();
            sessionProvider.CloseCurrent();
        }
    }
}