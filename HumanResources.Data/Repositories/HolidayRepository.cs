using System;
using HumanResources.Data.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.RepositoryContracts;

namespace HumanResources.Data.Repositories
{
    public class HolidayRepository : Repository<Holiday, Guid>, IHolidayRepository
    {
        public HolidayRepository(ISessionProvider sessionProvider) :
            base(sessionProvider)
        {
        }
    }
}
