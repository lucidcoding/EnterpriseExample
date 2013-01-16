using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HumanResources.Data.Core;
using HumanResources.Validation.Contracts;
using HumanResources.Validation.Implemetations;
using StructureMap.Configuration.DSL;

namespace HumanResources.Validation.Core
{
    public class ValidationRegistry : Registry
    {
        public ValidationRegistry()
        {
            Configure(x =>
            {
                For<IHolidayValidator>().Use<HolidayValidator>();
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}