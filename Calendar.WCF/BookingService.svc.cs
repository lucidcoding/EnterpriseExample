using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Calendar.Application.Requests;
using Calendar.Data.Common;
using Calendar.WCF.Core;
using Calendar.WCF.DataTransferObjects;
using Calendar.WCF.Mappers;
using StructureMap;

namespace Calendar.WCF
{
    //[SessionClosingBehaviour]
    public class BookingService : IBookingService
    {
        private readonly Application.Contracts.IBookingService _bookingService;
        private readonly ISessionProvider _sessionProvider;

        //todo: there is a better way to do IOC with WCF, but haven't got it working.
        public BookingService()
        {
            //ObjectFactory.Container.Configure(x => x.AddRegistry<WcfRegistry>());
            _bookingService = ObjectFactory.GetInstance<Application.Contracts.IBookingService>();
            _sessionProvider = ObjectFactory.GetInstance<ISessionProvider>();
        }

        public BookingDto[] Search(SearchBookingsRequest request)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var bookingDtos = new BookingDtoMapper().Map(_bookingService.Search(request).ToArray());
                    transactionScope.Complete();
                    return bookingDtos;
                }
            }
            finally
            {
                _sessionProvider.CloseCurrent();
            }
        }

        public BookingDto GetById(Guid id)
        {
            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var bookingDto = new BookingDtoMapper().Map(_bookingService.GetById(id));
                    transactionScope.Complete();
                    return bookingDto;
                }
            }
            finally
            {
                _sessionProvider.CloseCurrent();
            }
        }
    }
}
