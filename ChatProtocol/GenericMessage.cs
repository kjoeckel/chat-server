using System;

namespace ChatProtocol
{
    public class GenericMessage : IMessage
    {
        public string SessionId { get; set; }
        public int MessageId { get; set; }

        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }


        public GenericMessage(int messageId)
        {
            MessageId = messageId;
        }
    }
}
