using System;
using System.ServiceModel;
using HumanResources.WCF.DataTransferObjects;

namespace HumanResources.WCF
{
    [ServiceContract]
    public interface IDepartmentService
    {
        [OperationContract]
        DepartmentDto GetByIdWithManager(Guid id);
    }
}
