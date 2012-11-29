using System;
using System.Collections.Generic;
using Calendar.Application.Contracts;
using Calendar.Application.Requests;
using Calendar.Domain.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.Events;
using Calendar.Domain.RepositoryContracts;
using Calendar.Messages.Events;
using NServiceBus;

namespace Calendar.Application.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBus _bus;
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingTypeRepository _bookingTypeRepository;

        public BookingService(
            IBus bus,
            IBookingRepository bookingRepository,
            IBookingTypeRepository bookingTypeRepository)
        {
            _bus = bus;
            _bookingRepository = bookingRepository;
            _bookingTypeRepository = bookingTypeRepository;
        }

        public ValidationMessageCollection ValidateMake(MakeBookingRequest request)
        {
            var otherBookings = _bookingRepository.GetByEmployeeId(request.EmployeeId);
            return Booking.ValidateMake(otherBookings, request.Start, request.End);
        }

        public void Make(MakeBookingRequest request)
        {
            var otherBookings = _bookingRepository.GetByEmployeeId(request.EmployeeId);
            var bookingType = _bookingTypeRepository.GetById(request.BookingTypeId);
            DomainEvents.Register<BookingMadeEvent>(BookingMade);
            Booking.Make(request.Id, request.EmployeeId, otherBookings, request.Start, request.End, bookingType);
        }

        private void BookingMade(BookingMadeEvent @event)
        {
            _bookingRepository.Save(@event.Source);

            var bookingMadeEvent = new BookingMade
            {
                Id = @event.Source.Id.Value,
                EmployeeId = @event.Source.Id.Value,
                Start = @event.Source.Start,
                End = @event.Source.End,
                BookingTypeId = @event.Source.BookingType.Id.Value
            };

            _bus.Send(bookingMadeEvent);
        }

        public IEnumerable<Booking> Search(SearchBookingsRequest request)
        {
            var bookings = _bookingRepository.Search(request.EmployeeId, request.Start, request.End);
            return bookings;
        }

        public Booking GetById(Guid id)
        {
            var booking = _bookingRepository.GetById(id);
            return booking;
        }
    }
}
