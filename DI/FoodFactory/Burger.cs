using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodFactory
{
    internal class Burger : IFood
    {
        public void prepare()
        {
            Console.WriteLine("Preparing Burger....");
        }
    }
}
