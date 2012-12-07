﻿using NServiceBus;
using Sales.Data.Core;
using Sales.Data.Repositories;
using Sales.Domain.RepositoryContracts;
using Sales.UI.HumanResources.WCF;
using StructureMap.Configuration.DSL;

namespace Sales.UI.Core
{
    public class UiRegistry : Registry
    {
        public UiRegistry()
        {
            Configure(x =>
            {
                For<IBus>().Use(MvcApplication.Bus);
                For<ILeadRepository>().Use<LeadRepository>();
                For<IEmployeeService>().Use(new EmployeeServiceClient());
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}