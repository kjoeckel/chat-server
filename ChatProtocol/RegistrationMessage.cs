using System;
using System.Collections.Generic;
using System.Text;

namespace ChatProtocol 
{
    public class RegistrationMessage : IMessage
    {
        public string ServerPassword { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int MessageId
        {
            get
            {
                return 6;
            }
            set { }
        }
    }
}
