using NHibernate;
using Sales.Data.Common;
using Sales.Data.Repositories;
using Sales.Domain.RepositoryContracts;
using StructureMap.Configuration.DSL;

namespace Sales.Data.Core
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            Configure(x =>
            {
                For<ILeadRepository>().Use<LeadRepository>();
                For<IVisitRepository>().Use<VisitRepository>();
                For<IDealRepository>().Use<DealRepository>();
                For<ISessionProvider>().Use<SessionProvider>();
                For<ISessionFactory>().Singleton().Use(SessionFactoryFactory.GetSessionFactory());
            });
        }
    }
}