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
        EmployeeDto[] GetByIds(Guid[] ids);

        [OperationContract]
        EmployeeDto GetById(Guid id);

        [OperationContract]
        EmployeeDto[] GetCurrentByDepartmentId(Guid departmentId);
    }
}
