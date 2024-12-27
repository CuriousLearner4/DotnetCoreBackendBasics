using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TuplesPOC
{
    public static class Extenstions
    {
        //my extension function
        public static (int Min, int Max)? MinAndMax(this List<int> numbers)
        {
            if (numbers == null || numbers.Count == 0)
            {
                return null;
            }
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (var number in numbers)
            {
                min = int.Min(number, min);
                max = int.Max(number, max);
            }
            return (Min:min, Max: max);
        }
    }
    internal class Program
    {
        
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello, World!");
            (double, int) t1 = (4.5, 3);
            (double, int) t2 = (4.5, 3);
            Console.WriteLine("Equality of two tuples {0}", t1 == t2);
            Console.WriteLine($"item1 : {t1.Item1},item 2:{t1.Item2}");
            t1.Item2 = 45;
            Console.WriteLine($"item1 : {t1.Item1},item 2:{t1.Item2}");
            var numbers = new List<int>() { 8, 12, 15, 3, 5, 18,1 };
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var minandmax = numbers.MinAndMax();
            (int Min, int Max)? t3 = minandmax;
            Console.WriteLine(stopwatch.ElapsedTicks);
            stopwatch.Stop();
            Console.WriteLine($"{minandmax}");
            var limitsLookup = new Dictionary<int, (int Min, int Max)>()
            {
                [2] = (4,10),
                [4] = (10,20),
                [6] = (0,23)
            };
            if (limitsLookup.TryGetValue(4, out (int Min, int Max) limits))
            {
                Console.WriteLine($"Found limits: min is {limits.Min}, max is {limits.Max}");
            }
        }
    }
}
