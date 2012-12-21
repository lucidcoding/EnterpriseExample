using System;
using Finance.Domain.Common;
using Finance.Domain.Enumerations;

namespace Finance.Domain.Entities
{
    public class Installment : Entity<Guid>
    {
        public virtual DateTime DueDate { get; set; }
        public virtual int Amount { get; set; }
        public virtual bool Paid { get; set; }
        public virtual Account Account { get; set; }

        public static Installment Create(DateTime dueDate, int amount, Account account)
        {
            return new Installment
                       {
                           Id = Guid.NewGuid(),
                           DueDate = dueDate,
                           Amount = amount,
                           Paid = false,
                           Account = account
                       };
        }

        public virtual void MarkAsPaid()
        {
            Paid = true;
        }

        public virtual InstallmentStatus Status
        {
            get
            {
                if (Paid)
                {
                    return InstallmentStatus.Paid;
                }

                if(Account.Status == AccountStatus.Closed)
                {
                    return InstallmentStatus.NotRequired;
                }

                if (DateTime.Now > DueDate.AddDays(10))
                {
                    return InstallmentStatus.Overdue;
                }

                if (DateTime.Now > DueDate)
                {
                    return InstallmentStatus.Due;
                }

                return InstallmentStatus.Pending;
            }
        }
    }
}
