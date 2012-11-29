﻿using Calendar.Messages.Events;
using NServiceBus;

namespace HumanResources.MessageHandlers.Core
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
            _bus.Subscribe<BookingMade>();
            _bus.Subscribe<BookingUpdated>();
            _bus.Subscribe<MakeBookingInvalidated>();
            _bus.Subscribe<UpdateBookingInvalidated>();
        }

        public void Stop()
        {
            _bus.Unsubscribe<BookingMade>();
            _bus.Unsubscribe<BookingUpdated>();
            _bus.Unsubscribe<MakeBookingInvalidated>();
            _bus.Unsubscribe<UpdateBookingInvalidated>();
        }
    }
}
