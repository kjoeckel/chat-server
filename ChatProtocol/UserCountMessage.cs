using System.Collections.Generic;

namespace ChatProtocol
{
    public class UserCountMessage : IMessage
    {
        public int UserCount { get; set; }
        public int UserOnlineCount { get; set; }
        public List<string> UserNames { get; set; }

        public int MessageId
        {
            get
            {
                return 5;
            }
            set { }
        }
    }
}
