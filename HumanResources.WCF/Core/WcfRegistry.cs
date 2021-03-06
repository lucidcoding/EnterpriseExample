﻿using HumanResources.Data.Core;
using StructureMap.Configuration.DSL;

namespace HumanResources.WCF.Core
{
    public class WcfRegistry : Registry
    {
        public WcfRegistry()
        {
            Configure(x =>
                          {
                              For<IEmployeeService>().Use<EmployeeService>();
                              x.ImportRegistry(typeof (DataRegistry));
                          });
        }
    }
}