using System.Linq;
using System.Text;
using ItOps.MessageHandlers.Functionality.Emails;
using ItOps.MessageHandlers.Globals;
using ItOps.MessageHandlers.HumanResources.WCF;
using ItOps.MessageHandlers.Sales.WCF;
using NServiceBus;
using Sales.Messages.Events;

namespace ItOps.MessageHandlers.EventHandlers
{
    public class LeadsUnassignedHandler : IHandleMessages<LeadsUnassigned>
    {
        private readonly ILeadService _leadService;
        private readonly IDepartmentService _departmentService;
        private readonly IEmailSender _emailSender;

        public LeadsUnassignedHandler(
            ILeadService leadService,
            IDepartmentService departmentService,
            IEmailSender emailSender)
        {
            _leadService = leadService;
            _departmentService = departmentService;
            _emailSender = emailSender;
        }

        public void Handle(LeadsUnassigned message)
        {
            var leads = _leadService.GetByIds(message.UnassignedLeadsIds.ToArray());
            var department = _departmentService.GetByIdWithManager(Constants.SalesDepartmentId);

            var body = new StringBuilder();
            body.AppendLine("The following leads have been unassigned due to the consultant recently leaving");
            body.AppendLine();

            foreach(var lead in leads)
            {
                body.AppendLine(lead.Name + ", " + 
                    lead.Address1 + ", " + 
                    lead.Address2 + ", " +
                    lead.Address3 + ", ");
            }

            _emailSender.To.Add(department.Manager.EmailAddress);
            _emailSender.From = Constants.SenderEmailAddress;
            _emailSender.Body = body.ToString();
            _emailSender.Send();
        }
    }
}
