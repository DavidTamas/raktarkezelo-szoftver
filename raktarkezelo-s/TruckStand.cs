using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    class TruckStand
    {
        private static int CurrentID = 0;
        public int ID { get; private set; }
        public Goods Goods { get; private set; }

        public TruckStand()
        {
            ID = CurrentID++;
            Goods = null;
        }

        public void AddGoods(Goods goods)
        {
            Goods = goods;
        }

        public void RemoveGoods()
        {
            Goods = null;
        }

        public string Print()
        {
            string print = "";
            print += "-- Truck Stand " + ID + ": --\n";
            if (Goods != null)
            {
                print += "ID\tClient\tDescription Amount\tCooling\n";
                print += Goods.PrintWithClient();
            }
            else
                print += "No goods\n";
            return print;
        }
    }
}
