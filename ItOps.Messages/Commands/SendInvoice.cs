using System;
using NServiceBus;

namespace ItOps.Messages.Commands
{
    public class SendInvoice : IMessage
    {
        public Guid InstallmentId { get; set; }
    }
}
