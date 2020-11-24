using ChatProtocol;
using MoreLinq;
using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text.Json;

namespace ChatServer.MessageHandler
{
    class RegistrationMessageHandler : IMessageHandler
    {
        public void Execute(Server server, TcpClient client, IMessage message)
        {
            var registrationMessage = message as RegistrationMessage;

            bool authenticatedServerPassword = true;
            if (server.HasPassword())
            {
                authenticatedServerPassword = server.CheckPassword(registrationMessage.ServerPassword);
            }
            var tmpUserList = server.GetUsers();
            int newId = (tmpUserList.Select(x => x.Id).Max()) + 1;
            var user = new User()
            {
                Username = registrationMessage.Username,
                Password = registrationMessage.Password,
                Id = newId
            };

            server.AddUsers(user);
            string userJson = JsonSerializer.Serialize(server.GetUsers());
            File.WriteAllText("users.json", userJson);
            bool authenticatedUser = true;

            bool authenticated = authenticatedServerPassword && authenticatedUser;
            ConnectResponseMessage connectResponseMessage = new ConnectResponseMessage();
            if (authenticated)
            {
                string sessionId = Guid.NewGuid().ToString();
                user.SessionIds.Add(sessionId);
                connectResponseMessage.SessionId = sessionId;
                server.AddClient(client);
                Console.WriteLine("Client connected.");

                if (user.SessionIds.Count == 1)
                {

                    // Send user count to all clients (broadcast)
                    UserCountMessage userCountMessage = new UserCountMessage
                    {
                        UserCount = server.GetUsers().Count,
                        UserOnlineCount = server.GetUsers().Count(u => u.SessionIds.Count > 0),
                        UserNames = server.GetOnlineUserNames()
                    };

                    string userCountMessageJson = JsonSerializer.Serialize(userCountMessage);
                    byte[] userCountMessageBytes = System.Text.Encoding.UTF8.GetBytes(userCountMessageJson);

                    foreach (TcpClient remoteClient in server.GetClients())
                    {
                        remoteClient.GetStream().Write(userCountMessageBytes, 0, userCountMessageBytes.Length);
                    }
                }
            }

            connectResponseMessage.Success = authenticated;

            string json = JsonSerializer.Serialize(connectResponseMessage);
            byte[] msg = System.Text.Encoding.UTF8.GetBytes(json);
            client.GetStream().Write(msg, 0, msg.Length);

            var registrationResponseMessage = new RegistrationResponseMessage();

            foreach (User usr in server.GetUsers())
            {
                registrationResponseMessage.Users.Add(new ChatProtocol.User(usr.Id, usr.Username));
            }

            string jsonReg = JsonSerializer.Serialize(registrationResponseMessage);
            byte[] regMsg = System.Text.Encoding.UTF8.GetBytes(jsonReg);
            client.GetStream().Write(regMsg, 0, regMsg.Length);
        }
    }
}
