using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    [Serializable]
    class Goods
    {
        private static int CurrentID = 0;
        public int ID { get; private set; }
        public Client Client { get; private set; }
        public string Description { get; private set; }
        public int Amount { get; private set; }
        public bool RequiresCooling { get; private set; }
        public bool IsStored { get; set; }

        public Goods() { }
        public Goods(Client client, string description, int amount, bool requiresCooling)
        {
            ID = CurrentID++;
            Client = client;
            Description = description;
            Amount = amount;
            RequiresCooling = requiresCooling;
            IsStored = false;
        }

        public void Print()
        {
            Console.WriteLine(ID + "\t" + Description + "\t" + Amount + "\t" + RequiresCooling + "\t" + IsStored);
        }

        public void PrintWithClient()
        {
            Console.WriteLine(ID + "\t" + Client.Email + "\t" + Description + "\t" + Amount + "\t" + RequiresCooling);
        }
    }
}
