using System;
using System.Collections.Generic;
using ClientServices.Domain.Common;
using ClientServices.Domain.Enumerations;
using ClientServices.Domain.Events;

namespace ClientServices.Domain.Entities
{
    public class Agreement : Entity<Guid>
    {
        public virtual Client Client { get; set; }
        public virtual DateTime Commencement { get; set; }
        public virtual DateTime Expiry { get; set; }
        public virtual int Value { get; set; }
        public virtual IList<Service> Services { get; set; }
        public virtual AgreementStatus Status { get; set; }

        public static Agreement Initialize(
            Guid id, 
            Client client, 
            DateTime commencement, 
            DateTime expiry, 
            int value, 
            IList<Service> services)
        {
            var agreement = new Agreement
                                {
                                    Id = id,
                                    Client = client,
                                    Commencement = commencement,
                                    Expiry = expiry,
                                    Value = value,
                                    Services = services,
                                    Status = AgreementStatus.Initialized
                                };

            return agreement;
        }

        public virtual void Activate()
        {
            Status = AgreementStatus.Active;
            DomainEvents.Raise(new AgreementActivatedEvent(this));
        }

        public virtual void Cancel()
        {
            Status = AgreementStatus.Cancelled;
            DomainEvents.Raise(new AgreementCancelledEvent(this));
        }

        public virtual void Suspend()
        {
            Status = AgreementStatus.Suspended;
            DomainEvents.Raise(new AgreementSuspendedEvent(this));
        }
    }
}
