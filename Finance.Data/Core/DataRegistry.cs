using NHibernate;
using Finance.Data.Common;
using Finance.Data.Repositories;
using Finance.Domain.RepositoryContracts;
using StructureMap.Configuration.DSL;

namespace Finance.Data.Core
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            Configure(x =>
            {
                For<IAccountRepository>().Use<AccountRepository>();
                For<IInstallmentRepository>().Use<InstallmentRepository>();
                For<ISessionProvider>().Use<SessionProvider>();
                For<ISessionFactory>().Singleton().Use(SessionFactoryFactory.GetSessionFactory());
            });
        }
    }
}