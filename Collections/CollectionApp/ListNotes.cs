using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Program;

namespace CollectionApp
{
    class ListNotes
    {
        public static void ListFunctions()
        {
            Customer customer0 = new Customer()
            {
                ID = 1,
                Name = "Test",
                Age = 20,
            };
            Customer customer1 = new Customer()
            {
                ID = 2,
                Name = "Test1",
                Age = 10,
            };
            Customer customer2 = new Customer()
            {
                ID = 3,
                Name = "Test2",
                Age = 25,
            };
            List<Customer>  customers = new List<Customer>(2);
            customers.Add(customer0);
            customers[0] = customer1;
            Console.WriteLine(customers[0].Name);
            customers.Insert(0, customer2);
            customers.Add(customer0);

            Console.WriteLine(customers.IndexOf(customer0,1));
            Console.WriteLine(customers.Contains(customer2));//return true or false
            Console.WriteLine(customers.Exists(cust=>cust.Age==25));

            Customer c =  customers.Find(cust=>cust.Age>=20);
            Console.WriteLine(c.ID);

            Console.WriteLine(customers.FindIndex(1,2,cust=>cust.Age<=10));

            List<int> numbers = new List<int>() { 1,2,8,9,5,3,7 };
            Console.WriteLine("########");
            numbers.Sort(new Comparison<int>((i1, i2) => i2.CompareTo(i1)));
            
            foreach(int i in numbers)
            {
                Console.WriteLine(i);
            }
            SortByAge sortByAge = new SortByAge();
            customers.Sort(sortByAge);
            foreach(Customer customer in customers)
            {
                Console.WriteLine($"name:{customer.Name} age:{customer.Age}");
            }
        }
    }

    
}
