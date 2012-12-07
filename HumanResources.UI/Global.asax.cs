﻿using System.Web.Mvc;
using System.Web.Routing;
using HumanResources.UI.Common;
using HumanResources.UI.Core;
using NServiceBus;
using StructureMap;

namespace HumanResources.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        //public static IBus Bus { get; private set; }

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
                new { controller = "Employee", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Configure.WithWeb()
                .StructureMapBuilder()
                .ForMvc()
                .JsonSerializer()
                .Log4Net()
                .MsmqTransport()
                    .IsTransactional(false)
                    .PurgeOnStartup(false)
                .MsmqSubscriptionStorage()
                //.DoNotCreateQueues()
                .UnicastBus()
                    .ImpersonateSender(false)
                .CreateBus()
                .Start(() => Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());


            //var config = Configure.With();
            //config = config.StructureMapBuilder();
            //config = config.ForMvc();
            //config = config.JsonSerializer();
            //config = config.Log4Net();

            //config = config.MsmqTransport()
            //    .IsTransactional(false)
            //    .PurgeOnStartup(false);

            //config = config.MsmqSubscriptionStorage();
            //    //.DoNotCreateQueues()
            //config = config.UnicastBus();
            //    //    .ImpersonateSender(false)

            //var bus = config.CreateBus();
            //bus.Start(() => Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());

            ObjectFactory.Container.Configure(x => x.AddRegistry<UiRegistry>());
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerActivator());
        }
    }
}