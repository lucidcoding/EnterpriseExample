using Calendar.Application.Contracts;
using Calendar.Application.Implementations;
using Calendar.Data.Core;
using StructureMap.Configuration.DSL;
using NServiceBus;

namespace Calendar.Application.Core
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            Configure(x =>
            {
                //For<IAppointmentService>().Use<AppointmentService>();
                For<IBookingService>().Use<BookingService>();
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}

