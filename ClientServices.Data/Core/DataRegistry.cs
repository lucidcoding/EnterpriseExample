using NHibernate;
using ClientServices.Data.Common;
using ClientServices.Data.Repositories;
using ClientServices.Domain.RepositoryContracts;
using StructureMap.Configuration.DSL;

namespace ClientServices.Data.Core
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            Configure(x =>
            {
                For<IServiceRepository>().Use<ServiceRepository>();
                For<IClientRepository>().Use<ClientRepository>();
                For<IAgreementRepository>().Use<AgreementRepository>();
                For<ISessionProvider>().Use<SessionProvider>();
                For<ISessionFactory>().Singleton().Use(SessionFactoryFactory.GetSessionFactory());
            });
        }
    }
}