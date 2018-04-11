using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    [Serializable]
    class ImportNeed
    {
        private static int CurrentID = 0;
        public int ID { get; private set; }
        public DateTime Time { get; private set; }
        public Goods Goods { get; private set; }
        public DateTime BeginTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public ImportNeed() { }
        public ImportNeed(Goods goods, DateTime beginTime, DateTime endTime)
        {
            ID = ++CurrentID;
            Time = DateTime.Now;
            Goods = goods;
            BeginTime = beginTime;
            EndTime = endTime;
        }

        public void Print()
        {
            Console.WriteLine(ID + "\t" + Time + "\t" + Goods.ID + "\t" + Goods.Description + "\t" + Goods.Amount + "\t" + BeginTime + "\t" + EndTime);
        }
    }
}
