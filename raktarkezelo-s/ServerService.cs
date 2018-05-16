using Communication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace raktarkezelo
{
    public class AsyncService
    {
        private IPAddress ipAddress;
        private int port;

        public AsyncService(int port)
        {
            this.port = port;
            
            this.ipAddress = IPAddress.Loopback;
            if (this.ipAddress == null)
                throw new Exception("No IPv4 address for server");
        }

        public async void Run()
        {
            TcpListener listener = new TcpListener(this.ipAddress, this.port);
            listener.Start();
            Console.Write("Warehouse Management service is now running");
            Console.WriteLine(" " + this.ipAddress + " on port " + this.port);
            Console.WriteLine("Hit <enter> to stop service\n");
            while (true)
            {
                try
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    Task t = Process(tcpClient);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private KeyValuePair<bool, string> Execute(char usertype, char command, string message)
        {
            bool successful = false;
            if (usertype == 'C')
            {
                if (command == 'I')
                {
                    successful = ClientController.Instance.NewImportNeed(message);
                    message = "";
                }
                else if (command == 'E')
                {
                    successful = ClientController.Instance.NewExportNeed(message);
                    message = "";
                }
            }
            else if (usertype == 'K')
            {
                if (command == 'I')
                {
                    KeyValuePair<bool, string> pair = KeeperController.Instance.NewImport(message);
                    successful = pair.Key;
                    message = pair.Value;
                }
                else if (command == 'M')
                {
                    KeyValuePair<bool, string> pair = KeeperController.Instance.NewMoving(message);
                    successful = pair.Key;
                    message = pair.Value;
                }
                else if (command == 'J')
                {
                    KeyValuePair<bool, string> pair = KeeperController.Instance.ListImportNeeds();
                    successful = pair.Key;
                    message = pair.Value;
                }
                else if (command == 'K')
                {
                    KeyValuePair<bool, string> pair = KeeperController.Instance.ListImports();
                    successful = pair.Key;
                    message = pair.Value;
                }
            }
            return new KeyValuePair<bool, string>(successful, message);
        }

        private async Task Process(TcpClient tcpClient)
        {
            string clientEndPoint = tcpClient.Client.RemoteEndPoint.ToString();
            Console.WriteLine("Received connection request from " + clientEndPoint);
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                StreamReader reader = new StreamReader(networkStream);
                StreamWriter writer = new StreamWriter(networkStream);
                writer.AutoFlush = true;
                
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                while (true)
                {
                    string requestStr = await reader.ReadLineAsync();
                    if (requestStr != null)
                    {
                        CommObject request = serializer.Deserialize<CommObject>(requestStr);
                        Console.WriteLine("Received service request: " + request.Type + ": " + request + " (from " + request.UserType + ": " + clientEndPoint + ")");
                        KeyValuePair<bool, string> pair = Execute(request.UserType, request.Type, request.Message);
                        bool successful = pair.Key;
                        string message = pair.Value;
                        CommObject response = Response(request, successful, message);
                        Console.WriteLine("Computed response is: " + response + "\n");
                        await writer.WriteLineAsync(serializer.Serialize(response));
                    }
                    else
                    {
                        Console.WriteLine("Connection closed, client: " + clientEndPoint);
                        break; // Client closed connection
                    }
                }
                tcpClient.Close();
            }
            catch (Exception ex)
            {                
                if (tcpClient.Connected)
                {                    
                    tcpClient.Close();
                }
                Console.WriteLine(ex.InnerException.Message + " -> " + ex.Message);
                Console.WriteLine("Connection closed, client: " + clientEndPoint);
            }
        }

        private static CommObject Response(CommObject request, bool successful, string message)
        {
            string result;
            if (successful)
            {
                result = "Successful (Server)" + "\n\n" + message;
            }
            else
                result = "FAILED (Server)";
            CommObject response = new CommObject('S', 'R', result);
            return response;
        }
    }
}
