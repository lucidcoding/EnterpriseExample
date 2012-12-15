using System;
using System.Collections.Generic;
using Finance.Domain.Common;
using Finance.Domain.Enumerations;
using Finance.Domain.Events;

namespace Finance.Domain.Entities
{
    public class Account : Entity<Guid>
    {
        public virtual Guid ClientId { get; set; }
        public virtual Guid AgreementId { get; set; }
        public virtual DateTime Expiry { get; set; }
        public virtual int Value { get; set; }
        public virtual IList<Installment> Installments { get; set; }
        public virtual AccountStatus Status { get; set; }

        public static void Open(
            Guid clientId,
            Guid agreementId,
            DateTime commencement,
            DateTime expiry,
            int value)
        {
            var account = new Account
                              {
                                  Id = Guid.NewGuid(),
                                  ClientId = clientId,
                                  AgreementId = agreementId,
                                  Expiry = expiry,
                                  Value = value,
                                  Installments = new List<Installment>(),
                                  Status = AccountStatus.Open
                              };

            var numberOfInstallments = (expiry - commencement).Days/30;

            for(int i = 0; i < numberOfInstallments; i ++)
            {
                var installment = Installment.Create(
                    commencement.AddDays(i*30),
                    value/numberOfInstallments,
                    account);

                account.Installments.Add(installment);
            }

            DomainEvents.Raise(new AccountOpenedEvent(account));
        }

        public virtual void Suspend()
        {
            Status = AccountStatus.Suspended;
            DomainEvents.Raise(new AccountSuspendedEvent(this));
        }

        public virtual void Close()
        {
            Status = AccountStatus.Closed;
            DomainEvents.Raise(new AccountClosedEvent(this));
        }
    }
}
