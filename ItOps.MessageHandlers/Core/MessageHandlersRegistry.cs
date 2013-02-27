using ItOps.MessageHandlers.ClientServices.WCF;
using ItOps.MessageHandlers.Finance.WCF;
using ItOps.MessageHandlers.Functionality.Emails;
using ItOps.MessageHandlers.HumanResources.WCF;
using ItOps.MessageHandlers.Sales.WCF;
using StructureMap.Configuration.DSL;

namespace ItOps.MessageHandlers.Core
{
    public class MessageHandlersRegistry : Registry
    {
        public MessageHandlersRegistry()
        {
            Configure(x =>
                          {
                              For<ILeadService>().Use(new LeadServiceClient());
                              For<IDepartmentService>().Use(new DepartmentServiceClient());
                              For<IClientService>().Use(new ClientServiceClient());
                              For<IInstallmentService>().Use(new InstallmentServiceClient());
                              For<IEmailSender>().Use<ConsoleFakeEmailSender>();
                          });
        }
    }
}
