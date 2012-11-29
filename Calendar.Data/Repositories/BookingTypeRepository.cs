using System;
using Calendar.Data.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.RepositoryContracts;

namespace Calendar.Data.Repositories
{
    public class BookingTypeRepository : Repository<BookingType, Guid>, IBookingTypeRepository
    {
        public BookingTypeRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }
    }
}
