using System;
using NServiceBus;

namespace Finance.Messages.Commands
{
    public class SuspendAccount : IMessage
    {
        public Guid Id { get; set; }
    }
}
