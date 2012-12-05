using System;
using System.Collections.Generic;
using System.Transactions;
using Calendar.Messages.Commands;
using NHibernate;
using NHibernate.Context;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Application.Requests;
using Sales.Domain.Common;
using Sales.Domain.Entities;
using Sales.Domain.Events;
using Sales.Domain.Global;
using Sales.Domain.RepositoryContracts;

namespace Sales.Application.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IBus _bus;
        private readonly IVisitRepository _appointmentRepository;

        public AppointmentService(
            ISessionFactory sessionFactory,
            IBus bus,
            IVisitRepository appointmentRepository)
        {
            _sessionFactory = sessionFactory;
            _bus = bus;
            _appointmentRepository = appointmentRepository;
        }

        public Visit GetById(Guid id)
        {
            using (var transactionScope = new TransactionScope())
            {
                var appointment = _appointmentRepository.GetById(id);
                transactionScope.Complete();
                return appointment;
            }
        }

        public IList<Visit> GetByIds(List<Guid> ids)
        {
            using (var transactionScope = new TransactionScope())
            {
                var appointments = _appointmentRepository.GetByIds(ids);
                transactionScope.Complete();
                return appointments;
            }
        }

        public ValidationMessageCollection ValidateBook(BookAppointmentRequest request)
        {
            using (var transactionScope = new TransactionScope())
            {
                var validationMessages = Visit.ValidateBook(
                    request.ConsultantId,
                    request.Date,
                    request.StartTime,
                    request.EndTime,
                    request.LeadName,
                    request.Address);

                transactionScope.Complete();
                return validationMessages;
            }
        }

        public void Book(BookAppointmentRequest request)
        {
            using (var transactionScope = new TransactionScope())
            {
                DomainEvents.Register<LeadAddedEvent>(AppointmentBooked);
                Visit.Book(request.ConsultantId, request.Id, request.Date, request.StartTime, request.EndTime, request.LeadName, request.Address);
                _appointmentRepository.Flush();
                transactionScope.Complete();
            }
        }

        private void AppointmentBooked(LeadAddedEvent @event)
        {
            _appointmentRepository.Save(@event.Source);

            //todo: still a problem here.
            //Some logic has slipped into here on how to raise a command to make a booking.
            var makeBookingCommand = new MakeBooking
            {
                Id = @event.Source.Id.Value,
                EmployeeId = @event.Source.ConsultantId,
                Start = @event.Source.Date + @event.Source.StartTime,
                End = @event.Source.Date + @event.Source.EndTime,
                BookingTypeId = Constants.SalesAppointmentBookingTypeId
            };

            _bus.Send(makeBookingCommand);
        }

        public ValidationMessageCollection ValidateUpdate(UpdateAppointmentRequest request)
        {
            using (var transactionScope = new TransactionScope())
            {
                var appointment = _appointmentRepository.GetById(request.Id);

                var validationMessages = appointment.ValidateUpdate(
                    request.Date,
                    request.StartTime,
                    request.EndTime,
                    request.LeadName,
                    request.Address);

                transactionScope.Complete();
                return validationMessages;
            }
        }

        public void Update(UpdateAppointmentRequest request)
        {
            using (var transactionScope = new TransactionScope())
            {
                var appointment = _appointmentRepository.GetById(request.Id);
                DomainEvents.Register<AppointmentUpdatedEvent>(AppointmentUpdated);

                appointment.Update(
                    request.Date,
                    request.StartTime,
                    request.EndTime,
                    request.LeadName,
                    request.Address);

                _appointmentRepository.Flush();
                transactionScope.Complete();
            }
        }

        private void AppointmentUpdated(AppointmentUpdatedEvent @event)
        {
            _appointmentRepository.Update(@event.Source);

            //todo: still a problem here.
            //Some logic has slipped into here on how to raise a command to make a booking.
            var makeBookingCommand = new UpdateBooking
            {
                Id = @event.Source.Id.Value,
                Start = @event.Source.Date + @event.Source.StartTime,
                End = @event.Source.Date + @event.Source.EndTime,
            };

            _bus.Send(makeBookingCommand);
        }
    }
}
