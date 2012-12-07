using System;
using NServiceBus;

namespace HumanResources.Messages.Commands
{
    public class MarkEmployeeAsLeft : IMessage
    {
        public Guid Id { get; set; }
    }
}
