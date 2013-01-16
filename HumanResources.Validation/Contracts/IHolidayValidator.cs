using System;
using HumanResources.Domain.Common;

namespace HumanResources.Validation.Contracts
{
    public interface IHolidayValidator
    {
        ValidationMessageCollection ValidateBook(Guid employeeId, DateTime start, DateTime end, string description);
    }
}