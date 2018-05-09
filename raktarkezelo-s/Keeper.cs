using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarkezelo
{
    class Keeper
    {
        public string Email { get; private set; }
        public string Name { get; private set; }
        public TruckStand TruckStand { get; private set; }

        public Keeper(string email, string name, TruckStand truckStand)
        {
            Email = email;
            Name = name;
            TruckStand = truckStand;
        }

        public void Print()
        {
            Console.WriteLine(Email + "\t" + Name + "\t" + TruckStand.ID);
        }
    }
}
