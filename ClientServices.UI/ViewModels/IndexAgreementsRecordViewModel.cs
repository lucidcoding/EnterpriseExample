using System;
using ClientServices.Domain.Enumerations;

namespace ClientServices.UI.ViewModels
{
    public class IndexAgreementsRecordViewModel
    {
        public Guid Id { get; set; }
        public virtual DateTime Commencement { get; set; }
        public virtual DateTime Expiry { get; set; }
        public virtual int Value { get; set; }
        public virtual AgreementStatus Status { get; set; }
    }
}