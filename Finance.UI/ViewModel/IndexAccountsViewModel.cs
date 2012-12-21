using System;
using Finance.Domain.Enumerations;

namespace Finance.UI.ViewModel
{
    public class IndexAccountsViewModel
    {
        public Guid Id { get; set; }
        public string ClientReference { get; set; }
        public string ClientName { get; set; }
        public DateTime Expiry { get; set; }
        public int Value { get; set; }
        public AccountStatus Status { get; set; }
    }
}