using System.Collections.Generic;

namespace ItOps.MessageHandlers.Functionality.Emails
{
    public interface IEmailSender
    {
        IList<string> To { get; set; }
        IList<string> Cc { get; set; }
        IList<string> Bcc { get; set; }
        string From { get; set; }
        string Body { get; set; }
        void Send();
    }
}
