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
            LoginMenu();
            /*
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
            */
        }

        public void LoginMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("Please identify yourself!");
                Console.WriteLine("1 - Client");
                Console.WriteLine("2 - Keeper");
                Console.WriteLine("0 - Quit");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        ClientMenu();
                        break;
                    case 2:
                        KeeperMenu();
                        break;
                    default:
                        break;
                }
            }
        }

        public void ClientMenu()
        {
            Console.WriteLine("Welcome!");
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Submit new Import Need");
                Console.WriteLine("2 - Submit new Export Need");
                Console.WriteLine("3 - View list of Goods");
                Console.WriteLine("0 - Quit");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        NewImport();
                        break;
                    case 2:
                        NewExport();
                        break;
                    case 3:
                        ListGoods();
                        break;
                    default:
                        break;
                }
            }
        }

        public void NewImport()
        {
            string input = "";
            input += "TestClient";
            Console.WriteLine("Enter the brief description of the Goods you would like to import. Eg. Crate of Apples");
            input += "~" + Console.ReadLine();
            Console.WriteLine("Enter the amount you would like to import. Eg. 20");
            input += "~" + Console.ReadLine();
            Console.WriteLine("Do the Goods require cooling? true or false");
            input += "~" + Console.ReadLine();
            Console.WriteLine("Enter the desired date and time of the import. Eg. 2018.01.01. 12:00");
            input += "~" + Console.ReadLine();
            Console.WriteLine("Enter the deadline of the reservation. Eg. 2018.01.22. 14:00");
            input += "~" + Console.ReadLine();
            NewRequest('C', 'I', input);
        }

        public void NewExport()
        {
            string input = "";
            input += "TestClient";
            NewRequest('C', 'G', input);
            Console.WriteLine("Enter the ID of the Goods you would like to export.");
            input += "~" + Console.ReadLine();
            Console.WriteLine("Enter the desired date and time of the export. Eg. 2018.01.01. 12:00");
            input += "~" + Console.ReadLine();
            NewRequest('C', 'E', input);
        }

        public void ListGoods()
        {
            string input = "";
            input += "TestClient";
            NewRequest('C', 'G', input);
        }

        public void KeeperMenu()
        {
            Console.WriteLine("Welcome!");
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Register new Import");
                Console.WriteLine("2 - Register new Export");
                Console.WriteLine("3 - Register new Moving");
                Console.WriteLine("0 - Quit");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        RegisterImport();
                        break;
                    case 2:
                        RegisterExport();
                        break;
                    case 3:
                        MovingMenu();
                        break;
                    default:
                        break;
                }
            }
        }

        public void RegisterImport()
        {
            string input = "";
            input += "TestKeeper";
            NewRequest('K', 'J', "TestKeeper");
            Console.WriteLine("Enter the ID of the fulfilled Import Need.");
            input += "~" + Console.ReadLine();
            NewRequest('K', 'I', input);
        }

        public void RegisterExport()
        {
            string input = "";
            input += "TestKeeper";
            NewRequest('K', 'F', "TestKeeper");
            Console.WriteLine("Enter the ID of the fulfilled Export Need.");
            input += "~" + Console.ReadLine();
            NewRequest('K', 'E', input);
        }

        public void MovingMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Moving into Warehouse");
                Console.WriteLine("2 - Moving out from Warehouse");
                Console.WriteLine("3 - Moving inside Warehouse");
                Console.WriteLine("0 - Quit");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        RegisterMoving('I');
                        break;
                    case 2:
                        RegisterMoving('E');
                        break;
                    case 3:
                        RegisterMoving('M');
                        break;
                    default:
                        break;
                }
            }
        }

        public void RegisterMoving(char type)
        {
            string input = "";
            input += "TestKeeper";
            input += "~" + type;
            if (type == 'I')
            {
                NewRequest('K', 'K', "");
                Console.WriteLine("Enter the ID of the fulfilled Import.");
                input += "~" + Console.ReadLine();
                NewRequest('K', 'M', input);
                return;
            }
            else if (type == 'E')
            {
                NewRequest('K', 'F', "");
                Console.WriteLine("Enter the ID of the fulfilled Export Need.");
                input += "~" + Console.ReadLine();
                NewRequest('K', 'M', input);
                return;
            }
            else if (type == 'M')
            {
                input += "~";
                NewRequest('K', 'M', input);
                return;
            }
        }
    }
}
