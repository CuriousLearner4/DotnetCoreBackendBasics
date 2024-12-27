using PizzaDBF.Data;
using PizzaDBF.Models;

namespace PizzaDBF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetFullNameOfEveryCustomer();

        }

        public static void AddCustomerToDb(string FirstName,string Lastname) {
            using PizzaDbContext dbContext = new PizzaDbContext();
            dbContext.Customers.Add(new Models.Customer() { FirstName = FirstName, LastName = Lastname });
            dbContext.SaveChanges();
        }

        public static void GetFullNameOfEveryCustomer()
        {
            using PizzaDbContext dbContext = new PizzaDbContext();
            foreach (Customer c in dbContext.Customers)
            {
                Console.WriteLine($"Name: {c.FirstLast}");
            }
        }
    }
}
