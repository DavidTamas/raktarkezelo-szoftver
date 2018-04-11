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
        public LinkedList<Goods> Goods { get; private set; }

        public Client() { }
        public Client(string email, string name)
        {
            Email = email;
            Name = name;
            Goods = new LinkedList<Goods>();
        }

        public void AddGoods(Goods goods)
        {
            Goods.AddLast(goods);
        }

        public void Print()
        {
            Console.WriteLine(Email + "\t" + Name);
        }
    }
}
