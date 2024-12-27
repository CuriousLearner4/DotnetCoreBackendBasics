
namespace SOLID.WithoutSRP
{
    public class Rectangle
    {
        private int l;
        private int b;

        public Rectangle(int l,int b)
        {
            this.l = l;
            this.b = b;
        }

        public void draw()
        {
            for(int i = 0; i < b; ++i)
            {
                for(int j = 0; j < l; ++j)
                {
                    if (i == 0||i == b-1||j == 0||j == l-1)
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
        }

        public int area()
        {
            return l * b;
        }
    }
}
