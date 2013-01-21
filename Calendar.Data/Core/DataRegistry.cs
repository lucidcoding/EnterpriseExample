using Calendar.Data.Common;
using Calendar.Data.Repositories;
using Calendar.Domain.RepositoryContracts;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace Calendar.Data.Core
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            Configure(x =>
                          {
                              For<IAppointmentRepository>().Use<AppointmentRepository>();
                              For<ISessionProvider>().Use<SessionProvider>();
                              For<ISessionFactory>().Singleton().Use(SessionFactoryFactory.GetSessionFactory());
                          });
        }
    }
}