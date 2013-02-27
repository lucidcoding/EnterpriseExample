using System.Globalization;
using System.Text;
using ItOps.MessageHandlers.ClientServices.WCF;
using ItOps.MessageHandlers.Finance.WCF;
using ItOps.MessageHandlers.Functionality.Emails;
using ItOps.MessageHandlers.Globals;
using ItOps.Messages.Commands;
using NServiceBus;

namespace ItOps.MessageHandlers.CommandHandlers
{
    public class SendInvoiceHandler : IHandleMessages<SendInvoice>
    {
        private readonly IClientService _clientService;
        private readonly IInstallmentService _installmentService;
        private readonly IEmailSender _emailSender;

        public SendInvoiceHandler(
            IClientService clientService,
            IInstallmentService installmentService,
            IEmailSender emailSender)
        {
            _clientService = clientService;
            _installmentService = installmentService;
            _emailSender = emailSender;
        }

        public void Handle(SendInvoice message)
        {
            var installment = _installmentService.GetById(message.InstallmentId);
            var client = _clientService.GetById(installment.Account.ClientId);
            var body = new StringBuilder();

            body.AppendLine("Please pay " + installment.Amount.ToString(CultureInfo.InvariantCulture) + " by " +
                            installment.DueDate.ToString("dd/MM/yyyy"));

            _emailSender.To.Add(client.EmailAddress);
            _emailSender.From = Constants.SenderEmailAddress;
            _emailSender.Body = body.ToString();
            _emailSender.Send();
        }
    }
}
