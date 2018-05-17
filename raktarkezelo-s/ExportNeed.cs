using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    [Serializable]
    class ExportNeed
    {
        private static int CurrentID = 0;
        public int ID { get; private set; }
        public DateTime Time { get; private set; }
        public Goods Goods { get; private set; }
        public DateTime ExportTime { get; private set; }

        public ExportNeed() { }
        public ExportNeed(Goods goods, DateTime exportTime)
        {
            ID = CurrentID++;
            Time = DateTime.Now;
            Goods = goods;
            ExportTime = exportTime;
        }

        public string Print()
        {
            return ID + "\t" + Time + "\t" + Goods.ID + "\t" + Goods.Description + "\t" + Goods.Amount + "\t" + ExportTime + "\n";
        }
    }
}
