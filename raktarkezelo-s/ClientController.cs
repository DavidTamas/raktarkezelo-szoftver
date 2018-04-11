using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    class ClientController
    {
        public bool NewImportNeed(ImportNeed need)
        {
            bool successful = false;
            Warehouse.Instance.Clients[need.Goods.Client.Email].AddGoods(need.Goods);
            EventLog.Instance.AddImportNeed(need);
            Import import = new Import(need);
            EventLog.Instance.AddImport(import);
            EventLog.Instance.AddMoving(new Moving('I', import, null));
            Inventory.Instance.PrintAllGoods();
            successful = true;
            return successful;
        }

        public bool NewExportNeed(ExportNeed need)
        {
            bool successful = false;
            EventLog.Instance.AddExportNeed(need);
            EventLog.Instance.AddMoving(new Moving('E', null, need));
            EventLog.Instance.AddExport(new Export(need));
            Inventory.Instance.PrintAllGoods();
            successful = true;
            return successful;
        }
    }
}
