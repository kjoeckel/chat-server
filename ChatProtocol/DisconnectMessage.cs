using System;

namespace ChatProtocol
{
    public class DisconnectMessage : IMessage
    {
        public string SessionId { get; set; }
        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }
        public int MessageId
        {
            get
            {
                return 3;
            }
            set { }
        }
    }

    public class DisconnectMessageMatze : GenericMessage
    {
        public DisconnectMessageMatze() : base (3)
        {
        }
    }
}
