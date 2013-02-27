using System.Runtime.Serialization;

namespace Finance.Domain.Enumerations
{
    [DataContract]
    public enum InstallmentStatus
    {
        [EnumMember]
        Pending = 1,

        [EnumMember]
        Due = 2,

        [EnumMember]
        Overdue = 3,

        [EnumMember]
        Paid = 4,

        [EnumMember]
        NotRequired = 5
    }
}
