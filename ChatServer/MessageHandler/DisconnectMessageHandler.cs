using ChatProtocol;
using System;
using System.Net.Sockets;

namespace ChatServer.MessageHandler
{

    public class DisconnectMessageHandler : IMessageHandler
    {
        public bool IsClientConnected = true;

        public void Execute(Server server, TcpClient client, IMessage message)
        {
            DisconnectMessage disconnectMessage = message as DisconnectMessage;
            IsClientConnected = false;

            Console.WriteLine($"{disconnectMessage.SessionId} Disconnecting");
        }
    }

}
