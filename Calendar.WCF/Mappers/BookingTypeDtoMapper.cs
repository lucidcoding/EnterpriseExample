using System.Collections.Generic;
using System.Linq;
using Calendar.Domain.Entities;
using Calendar.WCF.DataTransferObjects;

namespace Calendar.WCF.Mappers
{
    public class BookingTypeDtoMapper
    {
        public BookingTypeDto Map(BookingType entity)
        {
            return new BookingTypeDto
                       {
                           Id = entity.Id.Value,
                           Description = entity.Description
                       };
        }

        public BookingTypeDto[] Map(BookingType[] entities)
        {
            return entities.Select(Map).ToArray();
        }
    }
}