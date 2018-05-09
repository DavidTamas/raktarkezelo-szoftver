using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    [Serializable]
    class Moving
    {
        private static int CurrentID = 0;
        public int ID { get; }
        public char Type { get; }
        public DateTime Time { get; }
        public Import Import { get; }
        public ExportNeed ExportNeed { get; }

        public Moving() { }
        public Moving(char type, Import import, ExportNeed exportNeed)
        {
            ID = CurrentID++;
            Type = type;
            Time = DateTime.Now;
            if (Type == 'I')
            {
                Import = import;
                ExportNeed = null;
            }
            else if (Type == 'E')
            {
                ExportNeed = exportNeed;
                Import = null;
            }
            else if (Type == 'M')
            {
                Import = null;
                ExportNeed = null;
            }
        }

        public void Print()
        {
            Console.Write(ID + "\t" + Time + "\t" + Type + "\t");
            if (Type == 'I')
                Console.Write(Import.ID + "\t" + Import.ImportNeed.Goods.ID + "\t" + Import.ImportNeed.Goods.Description + "\t" + Import.ImportNeed.Goods.Amount);
            else if (Type == 'E')
                Console.Write(ExportNeed.ID + "\t" + ExportNeed.Goods.ID + "\t" + ExportNeed.Goods.Description + "\t" + ExportNeed.Goods.Amount);
            Console.WriteLine();
        }
    }
}
