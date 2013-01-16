using System;
using NServiceBus;

namespace Sales.Messages.Commands
{
    public class CompleteVisit : IMessage
    {
        public Guid Id { get; set; }
    }
}
