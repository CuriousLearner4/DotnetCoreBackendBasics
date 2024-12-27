using SOLID.SRP;
namespace GUIApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SOLID.SRP.RectangleGUI rectangle = new RectangleGUI(10, 7);
            rectangle.draw();
        }
    }
}
