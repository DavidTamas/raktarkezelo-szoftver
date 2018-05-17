using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    class ClientController
    {
        private static readonly ClientController instance = new ClientController();
        public static ClientController Instance
        {
            get { return instance; }
        }

        private ClientController() { }

        public bool NewImportNeed(string message)
        {
            bool successful = false;

            //parsing message
            List<string> parameters = message.Split('~').ToList();
            string email = parameters[0];
            string description = parameters[1];
            int amount = int.Parse(parameters[2]);
            bool requiresCooling = bool.Parse(parameters[3]);
            DateTime importTime = DateTime.Parse(parameters[4]);
            DateTime exportTime = DateTime.Parse(parameters[5]);

            //generating objects
            Goods goods = new Goods(Warehouse.Instance.Clients[email], description, amount, requiresCooling);
            ImportNeed need = new ImportNeed(goods, importTime, exportTime);
            EventContainer.Instance.AddImportNeed(need);
            Warehouse.Instance.Clients[need.Goods.Client.Email].AddGoods(need.Goods);

            successful = true;
            return successful;
        }

        public bool NewExportNeed(string message)
        {
            bool successful = false;

            //parsing message
            List<string> parameters = message.Split('~').ToList();
            string email = parameters[0];
            int goodsID = int.Parse(parameters[1]);
            DateTime time = DateTime.Parse(parameters[2]);

            //generating objects
            Goods goods = Warehouse.Instance.Clients[email].Goods[goodsID];
            ExportNeed need = new ExportNeed(goods, time);
            EventContainer.Instance.AddExportNeed(need);

            successful = true;
            return successful;
        }

        public KeyValuePair<bool, string> ListGoods(string message)
        {
            bool succesful = false;
            message = Warehouse.Instance.Clients[message].PrintGoods();
            succesful = true;
            return new KeyValuePair<bool, string>(succesful, message);
        }
    }
}
