using System;
using System.Collections.Generic;
using Calendar.Data.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.RepositoryContracts;
using NHibernate.Criterion;

namespace Calendar.Data.Repositories
{
    public class BookingRepository : Repository<Booking, Guid>, IBookingRepository
    {
        public BookingRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }

        public IEnumerable<Booking> GetByEmployeeId(Guid employeeId)
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Booking>()
                .Add(Restrictions.Eq("EmployeeId", employeeId))
                .List<Booking>();
        }

        public IEnumerable<Booking> Search(Guid employeeId, DateTime start, DateTime end)
        {
            return _sessionProvider
                .GetCurrent()
                .CreateCriteria<Booking>()
                .Add(Restrictions.Eq("EmployeeId", employeeId))
                .List<Booking>();

            //todo: employee should bee optional and needs rest of parameters.
        }
    }
}
