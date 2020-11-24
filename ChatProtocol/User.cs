using System;
using System.Collections.Generic;
using System.Text;

namespace ChatProtocol
{
    public class User
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
