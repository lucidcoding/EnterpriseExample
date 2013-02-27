using System.Runtime.Serialization;

namespace Finance.Domain.Enumerations
{
    [DataContract]
    public enum AccountStatus
    {
        [EnumMember]
        Open = 1,

        [EnumMember]
        Closed = 2,

        [EnumMember]
        Suspended = 3
    }
}
