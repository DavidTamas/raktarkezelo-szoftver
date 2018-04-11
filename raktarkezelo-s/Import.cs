using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    [Serializable]
    class Import
    {
        private static int CurrentID = 0;
        public int ID { get; private set; }
        public ImportNeed ImportNeed { get; private set; }
        public DateTime Time { get; private set; }

        public Import() { }
        public Import(ImportNeed importNeed)
        {
            ID = ++CurrentID;
            ImportNeed = importNeed;
            Time = DateTime.Now;
            ImportNeed.Goods.IsStored = true;
        }

        public void Print()
        {
            Console.WriteLine(ID + "\t" + ImportNeed.ID + "\t" + ImportNeed.Goods.ID + "\t" + ImportNeed.Goods.Description + "\t" + ImportNeed.Goods.Amount + "\t" + Time);
        }
    }
}
