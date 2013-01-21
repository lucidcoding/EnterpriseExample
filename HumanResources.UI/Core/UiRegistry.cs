using HumanResources.Data.Core;
using HumanResources.UI.Calendar.WCF;
using HumanResources.Validation.Core;
using StructureMap.Configuration.DSL;

namespace HumanResources.UI.Core
{
    public class UiRegistry : Registry
    {
        public UiRegistry()
        {
            Configure(x =>
            {
                For<IAppointmentService>().Use(new AppointmentServiceClient());
                x.ImportRegistry(typeof(ValidationRegistry));
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}