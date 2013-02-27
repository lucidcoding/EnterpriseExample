using System.Collections.Generic;
using System.Reflection;
using System.Text;
using log4net;

namespace ItOps.MessageHandlers.Functionality.Emails
{
    public class ConsoleFakeEmailSender : IEmailSender
    {
        public IList<string> To { get; set; }
        public IList<string> Cc { get; set; }
        public IList<string> Bcc { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ConsoleFakeEmailSender()
        {
            To = new List<string>();
            Cc = new List<string>();
            Bcc = new List<string>();
        }

        public void Send()
        {
            var toString = new StringBuilder();

            foreach(var to in To)
            {
                toString.Append(to + "; ");
            }

            var ccString = new StringBuilder();

            foreach (var cc in Cc)
            {
                ccString.Append(cc + "; ");
            }

            var bccString = new StringBuilder();

            foreach (var bcc in Bcc)
            {
                bccString.Append(bcc + "; ");
            }

            Log.Info("------------------------------- EMAIL -------------------------------");
            Log.Info("To: " + toString);
            Log.Info("CC: " + ccString);
            Log.Info("BCC: " + bccString);
            Log.Info("From: " + From);
            Log.Info("Body:");
            Log.Info(Body);
            Log.Info("---------------------------------------------------------------------");
        }
    }
}
