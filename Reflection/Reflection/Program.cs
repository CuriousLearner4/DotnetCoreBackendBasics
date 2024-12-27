using System.Reflection;

namespace Reflection
{
    public class Program
    {

        public static void Main(string[] args)
        {

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "q") break;
                Type? T = Type.GetType("Reflection." + input);
                //Properties in Customer class
                Console.WriteLine("Properties");
                PropertyInfo[] properties = T.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    Console.WriteLine(property.PropertyType.Name + "" + property.Name);
                }
                //Methods in customer class
                Console.WriteLine("Methods");
                MethodInfo[] methods = T.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    Console.WriteLine($"{method.ReturnType} + {method.Name}");
                }
                //constructor in customer class
                Console.WriteLine("Constructors");
                ConstructorInfo[] constructors = T.GetConstructors();
                foreach (ConstructorInfo constructor in constructors)
                {
                    Console.WriteLine(constructor.Name);
                }
            }

        }


    }

    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Degree { get; set; }

        public Student() { }
    }

}