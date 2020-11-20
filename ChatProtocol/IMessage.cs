using System;
using System.IO;

namespace ChatProtocol
{
    public interface IMessage
    {
        int MessageId { get; set; }
        string UserName { get; set; }
        DateTime TimeStamp { get; set; }



    }
}
