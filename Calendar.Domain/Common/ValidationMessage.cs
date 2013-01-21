using System.Runtime.Serialization;

namespace Calendar.Domain.Common
{
    [DataContract]
    public enum MessageType
    {
        [EnumMember]
        Info = 0,

        [EnumMember]
        Warning = 1,

        [EnumMember]
        Error = 2
    }

    [DataContract]
    public class ValidationMessage
    {
        protected MessageType _type;
        protected string _field;
        protected string _text;

        [DataMember]
        public MessageType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        [DataMember]
        public string Field
        {
            get { return _field; }
            set { _field = value; }
        }

        [DataMember]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public ValidationMessage(MessageType type, string text)
        {
            _type = type;
            _text = text;
        }

        public ValidationMessage(MessageType type, string field, string text)
        {
            _type = type;
            _field = field;
            _text = text;
        }
    }
}
