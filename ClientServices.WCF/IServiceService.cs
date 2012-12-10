using System.ServiceModel;
using ClientServices.WCF.DataTransferObjects;

namespace ClientServices.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceService" in both code and config file together.
    [ServiceContract]
    public interface IServiceService
    {
        [OperationContract]
        ServiceDto[] GetAll();
    }
}
