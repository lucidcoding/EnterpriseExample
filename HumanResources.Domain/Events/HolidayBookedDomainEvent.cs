using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Events
{
    public class HolidayBookedDomainEvent : DomainEvent<Holiday>
    {
        public HolidayBookedDomainEvent(Holiday holiday)
            : base(holiday)
        {
        }
    }
}
