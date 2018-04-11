using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    sealed class Warehouse
    {
        private static readonly Warehouse instance = new Warehouse();
        public static Warehouse Instance
        {
            get { return instance; }
        }
        private Inventory Inventory = Inventory.Instance;
        private EventLog EventLog = EventLog.Instance;
        public SortedDictionary<string, Client> Clients { get; private set; }

        private Warehouse()
        {
            Clients = new SortedDictionary<string, Client>();
        }

        public void AddClient(Client client)
        {
            Clients.Add(client.Email, client);
        }
    }
}
