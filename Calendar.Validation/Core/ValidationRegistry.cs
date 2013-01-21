using Calendar.Data.Core;
using Calendar.Validation.Contracts;
using Calendar.Validation.Implementations;
using StructureMap.Configuration.DSL;

namespace Calendar.Validation.Core
{
    public class ValidationRegistry : Registry
    {
        public ValidationRegistry()
        {
            Configure(x =>
            {
                For<IAppointmentValidator>().Use<AppointmentValidator>();
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}