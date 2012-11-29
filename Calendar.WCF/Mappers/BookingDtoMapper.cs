using System.Collections.Generic;
using System.Linq;
using Calendar.Domain.Entities;
using Calendar.WCF.DataTransferObjects;

namespace Calendar.WCF.Mappers
{
    public class BookingDtoMapper
    {
        public BookingDto Map(Booking entity)
        {
            return new BookingDto
                       {
                           Id = entity.Id.Value,
                           EmployeeId = entity.EmployeeId,
                           Start = entity.Start,
                           End = entity.End,
                           BookingType =
                               entity.BookingType != null ? new BookingTypeDtoMapper().Map(entity.BookingType) : null
                       };
        }

        public BookingDto[] Map(Booking[] entities)
        {
            return entities.Select(Map).ToArray();
        }
    }
}