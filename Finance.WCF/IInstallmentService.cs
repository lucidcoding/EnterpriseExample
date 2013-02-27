using System;
using System.ServiceModel;
using Finance.WCF.DataTransferObjects;

namespace Finance.WCF
{
    [ServiceContract]
    public interface IInstallmentService
    {
        [OperationContract]
        InstallmentDto GetById(Guid id);
    }
}
