using ChatProtocol;
using System.Net.Sockets;
using System.Text.Json;

namespace ChatServer.MessageHandler
{
    public class ChatMessageHandler : IMessageHandler
    {
        public void Execute(Server server, TcpClient client, IMessage message)
        {
            var chatMessage = message as ChatMessage;

            var user = server.GetUsers().Find(u => u.SessionIds.Contains(chatMessage.SessionId));

            if (user != null)
            {
                chatMessage.SenderName = user.Username;
                chatMessage.SessionId = null;
                var json = JsonSerializer.Serialize(chatMessage);
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(json);

                foreach (TcpClient remoteClient in server.GetClients())
                {
                    remoteClient.GetStream().Write(msg, 0, msg.Length);
                }
            }
        }
    }
}
