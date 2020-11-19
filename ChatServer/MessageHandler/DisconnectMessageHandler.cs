using ChatProtocol;
using System;
using System.Net.Sockets;

namespace ChatServer.MessageHandler
{

    public class DisconnectMessageHandler : IMessageHandler
    {
        public void Execute(Server server, TcpClient client, IMessage message)
        {
            DisconnectMessage disconnectMessage = message as DisconnectMessage;
            Console.WriteLine($"Disconnecting");
            
        }
    }

}
