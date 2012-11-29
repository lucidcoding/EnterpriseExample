using System;
using System.Collections.Generic;
using HumanResources.Application.Requests;
using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Application.Contracts
{
    public interface IHolidayService
    {
        IList<Holiday> GetByIds(List<Guid> ids);
        ValidationMessageCollection ValidateBook(BookHolidayRequest request);
        void Book(BookHolidayRequest request);
        ValidationMessageCollection ValidateUpdate(UpdateHolidayRequest request);
        void Update(UpdateHolidayRequest request);
    }
}
