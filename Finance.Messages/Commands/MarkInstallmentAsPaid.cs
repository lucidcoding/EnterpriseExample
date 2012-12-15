using System;
using NServiceBus;

namespace Finance.Messages.Commands
{
    public class MarkInstallmentAsPaid : IMessage
    {
        public Guid Id { get; set; }
    }
}
