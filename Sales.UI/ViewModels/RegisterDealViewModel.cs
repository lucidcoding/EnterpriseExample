using System;
using System.Web.Mvc;

namespace Sales.UI.ViewModels
{
    public class RegisterDealViewModel
    {
        public Guid Id { get; set; }
        public Guid LeadId { get; set; }
        public DateTime Commencement { get; set; }
        public DateTime Expiry { get; set; }
        public MultiSelectList Services { get; set; }
        public Guid[] ServiceIds { get; set; }
        public int Value { get; set; }
    }
}