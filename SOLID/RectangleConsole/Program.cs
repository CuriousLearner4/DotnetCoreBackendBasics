using SOLID.SRP;

namespace RectangleConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            SOLID.SRP.RectangleCompute rectangle = new RectangleCompute(8.5, 6.5);
            Console.WriteLine(rectangle.computeArea());
        }
    }
}
