using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Communication;

namespace AsynchronousClient
{
    public class SocketClient
    {
        private const string host = "127.0.0.1";
        private const int port = 50000;

        private static StreamWriter writer;
        private static StreamReader reader;
        private static TcpClient client;

        public static void StartClient()
        {
            try
            {
                //Server IP address
                IPAddress ipAddress = IPAddress.Loopback;

                if (ipAddress == null)
                    throw new Exception("No IPv4 address for server");
                client = new TcpClient();
                client.Connect(ipAddress, port); // Connect
                Console.WriteLine("Connect to server " + ipAddress + " on port " + port);
                NetworkStream networkStream = client.GetStream();
                writer = new StreamWriter(networkStream);
                reader = new StreamReader(networkStream);
                writer.AutoFlush = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }
        }

        public static void Close()
        {
            if (client.Connected)
            {
                client.Close();
            }
        }

        public static async Task<CommObject> SendRequest(CommObject data)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string requestData = serializer.Serialize(data);
                await writer.WriteLineAsync(requestData);
                string responseStr = await reader.ReadLineAsync();
                CommObject response = serializer.Deserialize<CommObject>(responseStr);
                return response;
            }
            catch (Exception ex)
            {
                return new CommObject(ex.Message);
            }
        }
    }
}
