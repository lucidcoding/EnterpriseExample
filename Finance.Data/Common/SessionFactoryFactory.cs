using NHibernate;
using NHibernate.Cfg;

namespace Finance.Data.Common
{
    public static class SessionFactoryFactory
    {
        public static ISessionFactory GetSessionFactory()
        {
            var configuration = new NHibernate.Cfg.Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(SessionFactoryFactory).Assembly.GetName().Name);
            log4net.Config.XmlConfigurator.Configure();
            var sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory;
        }
    }
}