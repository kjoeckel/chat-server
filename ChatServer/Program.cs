using ChatProtocol;
using ChatServer.MessageHandler;
using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;

namespace ChatServer
{
    public class Program
    {
        public static void HandleClient(Server server, TcpClient client)
        {
            byte[] bytes = new byte[256];
            bool clientIsConnected = true;

            while (clientIsConnected)
            {
                NetworkStream stream = client.GetStream();

                int jsonDataLength;

                while ((jsonDataLength = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string jsonData = System.Text.Encoding.UTF8.GetString(bytes, 0, jsonDataLength);

                    // Verschlüsselung: data entschlüsseln
                    GenericMessage genericMessage = JsonSerializer.Deserialize<GenericMessage>(jsonData);
                    IMessage message = MessageFactory.GetMessage(genericMessage.MessageId, jsonData);

                    IMessageHandler handler = MessageHandlerFactory.GetMessageHandler(genericMessage.MessageId);
                    handler.Execute(server, client, message);

                    if (message.MessageId == 3)
                    {
                        DisconnectMessageHandler disconnectMessageHandler = handler as DisconnectMessageHandler;
                        clientIsConnected = disconnectMessageHandler.IsClientConnected;
                    }
                }
                //if (!clientIsConnected)
                //{
                //    client.Close();
                //}
            }
        }

        static void Main()
        {
            Server server = new Server(13000, "127.0.0.1");

            try
            {
                server.SetPassword("test123");
                server.Start();

                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    Thread thread = new Thread(() => HandleClient(server, client));
                    thread.Start();
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}
