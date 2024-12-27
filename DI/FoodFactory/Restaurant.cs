using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodFactory
{
    internal class Restaurant
    {
        public IFood food;

        public IFood food1 { 
            set { 
                this.food = value;
            }
        }
        public Restaurant()
        {
        }
        public Restaurant(IFood food) {
            this.food = food;      
        }
        public void prepare(IFood? food2 = null)
        {
            if (food2 != null)
            {
                food2.prepare();
            }
            else
            {
                if (food != null)
                {
                    food.prepare();
                }
            }
            
        }

        public void prepareByMethodInjection(IFood food2) {
         
            food2.prepare();
        }
    }
}
