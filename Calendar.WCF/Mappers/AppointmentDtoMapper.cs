using System.Linq;
using Calendar.Domain.Entities;
using Calendar.WCF.DataTransferObjects;

namespace Calendar.WCF.Mappers
{
    public class AppointmentDtoMapper
    {
        public AppointmentDto Map(Appointment entity)
        {
            return new AppointmentDto
                       {
                           Id = entity.Id,
                           EmployeeId = entity.EmployeeId,
                           DepartmentId= entity.DepartmentId,
                           Start = entity.Start,
                           End = entity.End
                       };
        }

        public AppointmentDto[] Map(Appointment[] entities)
        {
            return entities.Select(Map).ToArray();
        }
    }
}