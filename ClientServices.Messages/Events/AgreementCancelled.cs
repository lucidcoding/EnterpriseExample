using System;
using NServiceBus;

namespace ClientServices.Messages.Events
{
    public class AgreementCancelled : IEvent
    {
        public Guid Id { get; set; }
    }
}
