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
        private EventContainer EventLog = EventContainer.Instance;
        public SortedDictionary<string, Client> Clients { get; private set; }
        public SortedDictionary<string, Keeper> Keepers { get; private set; }
        public List<TruckStand> TruckStands { get; private set; }

        private Warehouse()
        {
            Clients = new SortedDictionary<string, Client>();
            Keepers = new SortedDictionary<string, Keeper>();
            TruckStands = new List<TruckStand>();
            TruckStands.Add(new TruckStand());
            AddClient(new Client("TestClient", "Test Client"));
            AddKeeper(new Keeper("TestKeeper", "Test Keeper", TruckStands[0]));
        }

        public void AddClient(Client client)
        {
            Clients.Add(client.Email, client);
        }

        public void AddKeeper(Keeper keeper)
        {
            Keepers.Add(keeper.Email, keeper);
        }
    }
}
