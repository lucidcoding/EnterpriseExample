using System;
using System.ServiceModel;
using Calendar.Application.Requests;
using Calendar.WCF.DataTransferObjects;

namespace Calendar.WCF
{
    [ServiceContract]
    public interface IBookingService
    {
        [OperationContract]
        BookingDto[] Search(SearchBookingsRequest request);

        [OperationContract]
        BookingDto GetById(Guid id);
    }
}
