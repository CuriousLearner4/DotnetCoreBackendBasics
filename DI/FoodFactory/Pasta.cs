using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodFactory
{
    internal class Pasta : IFood
    {
        public void prepare()
        {
            Console.WriteLine("Preparing pasta...");
        }
    }
}
