using System;
using System.Collections.Generic;
using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.RepositoryContracts
{
    public interface IAppointmentRepository : IRepository<Appointment, Guid>
    {
        IList<Appointment> GetByEmployeeId(Guid employeeId);
    }
}
