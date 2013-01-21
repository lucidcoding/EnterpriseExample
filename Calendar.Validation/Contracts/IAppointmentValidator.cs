using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calendar.Domain.Common;

namespace Calendar.Validation.Contracts
{
    public interface IAppointmentValidator
    {
        ValidationMessageCollection ValidateBook(Guid employeeId, Guid departmentId, DateTime start, DateTime end, string description);
    }
}
