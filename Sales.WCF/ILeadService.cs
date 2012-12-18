using System;
using System.ServiceModel;
using Sales.WCF.DataTransferObjects;

namespace Sales.WCF
{
    [ServiceContract]
    public interface ILeadService
    {
        [OperationContract]
        LeadDto[] GetByIds(Guid[] ids);

        [OperationContract]
        LeadDto GetById(Guid id);
    }
}
