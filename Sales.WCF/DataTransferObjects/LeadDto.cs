using System;

namespace Sales.WCF.DataTransferObjects
{
    public class LeadDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? AssignedToConsultantId { get; set; }
        public bool SignedUp { get; set; }
    }
}