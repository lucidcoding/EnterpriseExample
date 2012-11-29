using System;
using System.ServiceModel;
using HumanResources.WCF.DataTransferObjects;

namespace HumanResources.WCF
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        EmployeeDto[] GetAll();

        [OperationContract]
        EmployeeDto GetById(Guid id);
    }
}
