namespace SOLID.SRP
{
    public class RectangleCompute
    {
        private double l;
        private double b;

        public RectangleCompute(double l, double b) { 
            this.l = l;
            this.b = b; 
        }

        public double computeArea() {
            return l * b;
        }

    }
}
