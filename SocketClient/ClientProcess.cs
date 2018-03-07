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
            Console.WriteLine("Enter requests, one per line. Enter \"Q\" to exit.\n");
            string input = Console.ReadLine();
            while (input != "Q")
            {
                CommObject request = new CommObject(input);

                Task<CommObject> tsResponse = SocketClient.SendRequest(request);
                Console.WriteLine("Sent request, waiting for response");
                CommObject dResponse = tsResponse.Result;
                Console.WriteLine("Received response: " + dResponse);
                input = Console.ReadLine();
            }
        }
    }
}
