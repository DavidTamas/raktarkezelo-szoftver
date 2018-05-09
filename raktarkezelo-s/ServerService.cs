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

        private bool Execute(char usertype, char command, string message)
        {
            bool successful = false;
            if (usertype == 'C')
            {
                if (command == 'I')
                    successful = ClientController.Instance.NewImportNeed(message);
                else if (command == 'E')
                    successful = ClientController.Instance.NewExportNeed(message);
            }
            else if (usertype == 'K')
            {
                if (command == 'I')
                    successful = KeeperController.Instance.NewImport(message);
                else if (command == 'M')
                    successful = KeeperController.Instance.NewMoving(message);
                else if (command == 'J')
                    successful = KeeperController.Instance.ListImportNeeds();
                else if (command == 'K')
                    successful = KeeperController.Instance.ListImports();
            }
            return successful;
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
                        bool successful = Execute(request.UserType, request.Type, request.Message);
                        CommObject response = Response(request, successful);
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

        private static CommObject Response(CommObject request, bool successful)
        {
            string result;
            if (successful)
                result = "Successful (Server)";
            else
                result = "FAILED (Server)";
            CommObject response = new CommObject('S', 'R', result);
            return response;
        }
    }
}
