using System.Runtime.CompilerServices;
using EF.Data;
using EF.Models;

namespace PizzaApp
{
    public class Program
    {
        static void Main(string[] args)
        {

            AddProductToDB("Chicken Maharaja Pizza", 250);
            AddProductToDB("Veg Supreme Pizza", 150);
            RemoveProductFromDB("Chicken Supreme Pizza");
            DisplayProducts();


        }

        public static void AddProductToDB(string item, decimal price)
        {
            using PizzaContext context = new PizzaContext();
            Product pizza = new Product() { Name = item, Price = price };
            context.Products.Add(pizza);
            context.SaveChanges();

        }

        public static void DisplayProducts()
        {
            try
            {
                using PizzaContext context = new PizzaContext();
                var products = from product in context.Products orderby product.Name select product;
                foreach (Product p in products)
                {
                    Console.WriteLine($"Id: {p.Id}");
                    Console.WriteLine($"Name: {p.Name}");
                    Console.WriteLine($"Price: {p.Price}");
                }
            }
            catch (NullReferenceException ex) {
                Console.WriteLine(ex);
            }

        }

        public static void RemoveProductFromDB(string item)
        {

            using PizzaContext context = new PizzaContext();
            var pizza = context.Products.Where(p => p.Name == item).FirstOrDefault();
            if (pizza is Product)
            {
                context.Products.Remove(pizza);
            }
            context.SaveChanges();
        }

    }
}

