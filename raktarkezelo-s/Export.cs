using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    [Serializable]
    class Export
    {
        private static int CurrentID = 0;
        public int ID { get; private set; }
        public ExportNeed ExportNeed { get; private set; }
        public DateTime Time { get; private set; }

        public Export() { }
        public Export(ExportNeed exportNeed)
        {
            ID = CurrentID++;
            ExportNeed = exportNeed;
            Time = DateTime.Now;
            ExportNeed.Goods.IsStored = false;
        }

        public void Print()
        {
            Console.WriteLine(ID + "\t" + ExportNeed.ID + "\t" + ExportNeed.Goods.ID + "\t" + ExportNeed.Goods.Description + "\t" + ExportNeed.Goods.Amount + "\t" + Time);
        }
    }
}
