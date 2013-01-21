using System;
using System.Collections.Generic;
using Calendar.Data.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.RepositoryContracts;
using NHibernate.Criterion;

namespace Calendar.Data.Repositories
{
    public class AppointmentRepository : Repository<Appointment, Guid>, IAppointmentRepository
    {
        public AppointmentRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }

        public IList<Appointment> GetByEmployeeId(Guid employeeId)
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Appointment>()
                .Add(Restrictions.Eq("EmployeeId", employeeId))
                .AddOrder(new Order("Start", true))
                .List<Appointment>();
        }
    }
}
