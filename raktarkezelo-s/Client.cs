using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    [Serializable]
    class Client
    {
        public string Email { get; }
        public string Name { get; }
        public SortedDictionary<int, Goods> Goods { get; private set; }

        public Client() { }
        public Client(string email, string name)
        {
            Email = email;
            Name = name;
            Goods = new SortedDictionary<int, Goods>();
        }

        public void AddGoods(Goods goods)
        {
            Goods.Add(goods.ID, goods);
        }

        public string Print()
        {
            return Email + "\t" + Name;
        }

        public string PrintGoods()
        {
            string print = "";
            print += "ID\tDescription Amount\tCooling\tStored\n";
            foreach (var i in Goods)
                print += i.Value.Print();
            return print;
        }
    }
}
