using System;
using System.Web.Mvc;

namespace Sales.UI.ViewModels
{
    public class RegisterDealViewModel
    {
        public Guid Id { get; set; }
        public Guid LeadId { get; set; }
        public SelectList Services { get; set; }
        public Guid? ServiceId { get; set; }
        public int Value { get; set; }
    }
}