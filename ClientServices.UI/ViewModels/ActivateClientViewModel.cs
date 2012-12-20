using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientServices.UI.ViewModels
{
    public class ActivateClientViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? LiasonEmployeeId { get; set; }
    }
}