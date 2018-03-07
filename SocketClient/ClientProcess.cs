using System;
using System.Threading.Tasks;
using AsynchronousClient;
using Communication;

namespace ClientProcess
{
    class Process
    {
        public Process() { }
        public void ReadAndWrite()
        {
            string data = Console.ReadLine();
            while (data != "Bye")
            {
                CommObject commObject = new CommObject(data);

                Task<CommObject> tsResponse = SocketClient.SendRequest(commObject);
                Console.WriteLine("Sent request, waiting for response");
                CommObject dResponse = tsResponse.Result;
                Console.WriteLine("Received response: " + dResponse);
                data = Console.ReadLine();
            }
        }
    }
}
