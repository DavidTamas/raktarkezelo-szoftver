using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    class KeeperController
    {
        private static readonly KeeperController instance = new KeeperController();
        public static KeeperController Instance
        {
            get { return instance; }
        }

        private KeeperController() { }

        public bool NewImport(string message)
        {
            bool successful = false;

            //parsing
            List<string> parameters = message.Split('~').ToList();
            string email = parameters[0];
            int needID = int.Parse(parameters[1]);

            //generating objects
            Import import = new Import(EventContainer.Instance.ImportNeeds[needID]);
            EventContainer.Instance.Imports.Add(import);
            Warehouse.Instance.Keepers[email].TruckStand.AddGoods(import.ImportNeed.Goods);

            //output
            Warehouse.Instance.Keepers[email].TruckStand.Print();

            successful = true;
            return successful;
        }

        public bool NewMoving(string message)
        {
            bool successful = false;

            //parsing
            List<string> parameters = message.Split('~').ToList();
            string email = parameters[0];
            char type = char.Parse(parameters[1]);
            int eventID = int.Parse(parameters[2]);

            //generating objects
            Moving Moving;
            if (type == 'I')
            {
                Moving = new Moving(type, EventContainer.Instance.Imports[eventID], null);
                Inventory.Instance.AddGoods(Moving.Import.ImportNeed.Goods);
                Warehouse.Instance.Keepers[email].TruckStand.RemoveGoods();
            }
            else if (type == 'E')
            {
                Moving = new Moving(type, null, EventContainer.Instance.ExportNeeds[eventID]);
                Inventory.Instance.RemoveGoods(Moving.ExportNeed.Goods);
                Warehouse.Instance.Keepers[email].TruckStand.AddGoods(Moving.ExportNeed.Goods);
            }
            else if (type == 'M')
            {
                Moving = new Moving(type, null, null);
            }

            //output
            Warehouse.Instance.Keepers[email].TruckStand.Print();
            Inventory.Instance.PrintAllGoods();

            successful = true;
            return successful;
        }

        public bool ListImportNeeds()
        {
            bool successful = false;
            EventContainer.Instance.PrintImportNeeds();
            successful = true;
            return successful;
        }

        public bool ListImports()
        {
            bool successful = false;
            EventContainer.Instance.PrintImports();
            successful = true;
            return successful;
        }
    }
}
