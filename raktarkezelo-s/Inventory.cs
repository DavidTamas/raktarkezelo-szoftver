using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    sealed class Inventory
    {
        private static readonly Inventory instance = new Inventory();
        public static Inventory Instance
        {
            get { return instance; }
        }
        public int TotalSlots { get; private set; }
        public int TotalCoolSlots { get; private set; }
        public LinkedList<Goods> CurrentGoods { get; private set; }
        public LinkedList<Goods> CurrentCoolGoods { get; private set; }
        public int GoodsAmount { get; private set; }
        public int CoolGoodsAmount { get; private set; }

        private Inventory()
        {
            CurrentGoods = new LinkedList<Goods>();
            CurrentCoolGoods = new LinkedList<Goods>();
			TotalSlots = 120;
			TotalCoolSlots = 60;
            GoodsAmount = 0;
            CoolGoodsAmount = 0;
        }

        public string PrintAllGoods()
        {
            string print = "";
            print += "-- INVENTORY --\n";
            print += "-- Normal Goods: " + GoodsAmount + " / " + TotalSlots + " --\n";
            print += "ID\tClient\tDescription Amount\tCooling\n";
            foreach(var i in CurrentGoods)
                print += i.PrintWithClient();
            print += "-- Cooled Goods: " + CoolGoodsAmount + " / " + TotalCoolSlots + " --\n";
            print += "ID\tClient\tDescription Amount\tCooling\n";
            foreach (var i in CurrentCoolGoods)
                print += i.PrintWithClient();
            print += "\n";
            return print;
        }

        public void AddGoods(Goods goods)
        {
            if(!goods.RequiresCooling)
            {
                CurrentGoods.AddLast(goods);
                GoodsAmount += goods.Amount;
            }
            else
            {
                CurrentCoolGoods.AddLast(goods);
                CoolGoodsAmount += goods.Amount;
            }
        }

        public void RemoveGoods(Goods goods)
        {
            if(!goods.RequiresCooling)
            {
                CurrentGoods.Remove(goods);
                GoodsAmount -= goods.Amount;
            }
            else
            {
                CurrentCoolGoods.Remove(goods);
                CoolGoodsAmount -= goods.Amount;
            }
        }
    }
}
