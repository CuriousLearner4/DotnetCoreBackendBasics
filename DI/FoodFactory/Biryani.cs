using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodFactory
{
    internal class Biryani : IFood
    {
        public void prepare()
        {
            Console.WriteLine("Preparing Biryani....");
        }
    }
}
