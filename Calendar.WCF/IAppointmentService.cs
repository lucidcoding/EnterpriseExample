using System;
using System.ServiceModel;
using Calendar.Domain.Common;
using Calendar.WCF.DataTransferObjects;
using Calendar.WCF.Requests;

namespace Calendar.WCF
{
    [ServiceContract]
    public interface IAppointmentService
    {
        [OperationContract]
        AppointmentDto[] GetByEmployeeId(Guid employeeId);


        [OperationContract]
        AppointmentDto[] GetByIds(Guid[] ids);

        [OperationContract]
        ValidationMessageCollection ValidateBook(ValidateBookRequest request);
    }
}
