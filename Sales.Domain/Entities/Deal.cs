using System;
using System.Collections.Generic;
using Sales.Domain.Common;
using Sales.Domain.Events;

namespace Sales.Domain.Entities
{
    public class Deal : Entity<Guid>
    {
        public virtual Lead Lead { get; set; }
        public virtual Guid MadeByConsultantId { get; set; }
        public virtual IList<DealService> Services { get; set; }
        public virtual int Value { get; set; }
        public virtual int Commission { get; set; }

        public static void RecordWasSigned(Lead lead, Guid madeByConsultantId, IList<Guid> serviceIds, int value)
        {
            var deal = new Deal
                           {
                               Id = Guid.NewGuid(),
                               MadeByConsultantId = madeByConsultantId,
                               Value = value,
                               Services = new List<DealService>(),
                               Commission = value/10
                           };

            foreach(var serviceId in serviceIds)
            {
                deal.Services.Add(new DealService
                                      {
                                          Id = Guid.NewGuid(),
                                          ServiceId = serviceId
                                      });
            }

            DomainEvents.Raise(new DealSignedEvent(deal));
        }
    }
}
