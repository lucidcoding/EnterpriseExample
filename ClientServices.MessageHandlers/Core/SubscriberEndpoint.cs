﻿using NServiceBus;
using HumanResources.Messages.Events;

namespace ClientServices.MessageHandlers.Core
{
    public class SubscriberEndpoint : IWantToRunAtStartup
    {
        private readonly IBus _bus;

        public SubscriberEndpoint(IBus bus)
        {
            _bus = bus;
        }

        public void Run()
        {
            _bus.Subscribe<EmployeeLeft>();
        }

        public void Stop()
        {
            _bus.Unsubscribe<EmployeeLeft>();
        }
    }
}
