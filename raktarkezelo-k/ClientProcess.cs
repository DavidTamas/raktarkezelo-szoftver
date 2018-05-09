using System;
using System.Threading.Tasks;
using AsynchronousClient;
using Communication;

namespace ClientProcess
{
    class Process
    {
        public Process() { }

        public void NewRequest(char usertype, char type, string input)
        {
            CommObject request = new CommObject(usertype, type, input);
            Task<CommObject> tsResponse = SocketClient.SendRequest(request);
            Console.WriteLine("Sent request, waiting for response");
            CommObject dResponse = tsResponse.Result;
            Console.WriteLine("Received response: " + dResponse);
        }

        public void ReadAndWrite()
        {
            Console.WriteLine("[C]lient or [K]eeper? Enter \"Q\" to exit.\n");
            char usertype = (char)Console.Read();
            Console.ReadLine();
            bool exit = false;
            if (usertype != 'C' && usertype != 'K')
                exit = true;
            while (!exit)
            {
                char type = '\0';
                string input = "";
                if (usertype == 'C')
                {
                    Console.WriteLine();
                    Console.WriteLine("[I]mport or [E]xport?");
                    type = (char)Console.Read();
                    Console.ReadLine();
                    if (type == 'I')
                    {
                        input = "TestClient";
                        input += "~desc~20~false~2018.10.01.~2018.10.01.";
                        NewRequest(usertype, type, input);
                    }
                    else
                        exit = true;
                }
                else if(usertype == 'K')
                {
                    Console.WriteLine();
                    Console.WriteLine("[I]mport, [E]xport or [M]oving?");
                    type = (char)Console.Read();
                    Console.ReadLine();
                    if (type == 'I')
                    {
                        NewRequest(usertype, 'J', "TestKeeper");
                        Console.WriteLine();
                        Console.WriteLine("Which Import Need has been fulfilled? (ID)");
                        input = "TestKeeper";
                        input += "~0";
                        NewRequest(usertype, 'I', input);
                    }
                    else if (type == 'M')
                    {
                        NewRequest(usertype, 'K', "TestKeeper");
                        Console.WriteLine();
                        Console.WriteLine("Moving [I]n (import) or [O]ut (export)?");
                        input = "TestKeeper";
                        input += "~I";
                        Console.WriteLine("Which Import has been fulfilled? (ID)");
                        input += "~0";
                        NewRequest(usertype, 'M', input);
                    }
                    else
                        exit = true;
                }
            }
        }
    }
}
