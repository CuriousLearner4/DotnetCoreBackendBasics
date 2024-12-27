using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodFactory
{
    internal class Waiter
    {
        public static void serve()
        {
            while (true)
            {
                Console.WriteLine("Select what do you want to eat\n1.Burger\n2.Biryani\n3.Pasta\n4.Exit");
                string? input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Injecting by Constructor");
                        Restaurant R1 = new Restaurant(new Burger());
                        R1.prepare();
                        break;
                    case "2":
                        Console.WriteLine("Injecting by Property");
                        Restaurant R2 = new Restaurant();
                        R2.food1 = new Biryani();
                        R2.prepare();
                        break;
                    case "3":
                        Console.WriteLine("Injecting by method");
                        Restaurant R3 = new Restaurant();
                        R3.prepare(new Pasta());
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Wrong input Try again");
                        break;
                }
            }
        }
    }
}
