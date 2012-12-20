using System;
using System.ServiceModel;
using ClientServices.WCF.DataTransferObjects;

namespace ClientServices.WCF
{
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        ClientDto GetById(Guid id);
        
        [OperationContract]
        ClientDto[] GetByIds(Guid[] ids);
    }
}
