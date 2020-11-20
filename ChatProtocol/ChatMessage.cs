using System;

namespace ChatProtocol
{
    public class ChatMessage : IMessage
    {
        public string SessionId { get; set; }
        public string Content { get; set; }

        public int MessageId
        {
            get
            {
                return 1;
            }
            set { }
        }

        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
