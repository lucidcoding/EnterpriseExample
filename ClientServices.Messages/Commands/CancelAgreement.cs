using System;
using NServiceBus;

namespace ClientServices.Messages.Commands
{
    public class CancelAgreement : IMessage
    {
        public Guid Id { get; set; }
    }
}
