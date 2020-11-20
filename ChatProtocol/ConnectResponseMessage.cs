using System;

namespace ChatProtocol
{
    public class ConnectResponseMessage : IMessage
    {
        public bool Success { get; set; }
        public string SessionId { get; set; }

        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }

        public int MessageId
        {
            get
            {
                return 4;
            }
            set { }
        }
    }
}
