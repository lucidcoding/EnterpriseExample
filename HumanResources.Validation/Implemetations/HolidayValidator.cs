using System;
using System.Transactions;
using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.RepositoryContracts;
using HumanResources.Validation.Contracts;

namespace HumanResources.Validation.Implemetations
{
    public class HolidayValidator : IHolidayValidator
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HolidayValidator(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ValidationMessageCollection ValidateBook(Guid employeeId, DateTime start, DateTime end, string description)
        {
            ValidationMessageCollection messages;

            using (var transactionScope = new TransactionScope())
            {
                var employee = _employeeRepository.GetById(employeeId);
                messages = Holiday.ValidateBook(employee, start, end, description);
                transactionScope.Complete();
            }

            return messages;
        }
    }
}