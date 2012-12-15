using System;
using NServiceBus;

namespace Finance.Messages.Events
{
    public class AccountSuspended : IEvent
    {
        public Guid Id { get; set; }
    }
}
