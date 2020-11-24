using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text.Json;


namespace ChatProtocol
{
    public class RegistrationResponseMessage : IMessage
    {
        public List<User> Users { get; set; }

        public RegistrationResponseMessage()
        {
            Users = new List<User>();
        }

        public int MessageId
        {
            get
            {
                return 7;
            }
            set { }
        }


    }
}
