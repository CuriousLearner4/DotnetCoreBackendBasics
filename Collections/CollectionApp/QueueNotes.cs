using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionApp
{
    public class QueueNotes
    {
        public static void QueueFuntions()
        {
            Queue<int> q = new Queue<int>();

            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);

            q.Dequeue();
            if(q.Count !=0)
            Console.WriteLine(q.Peek());
        }
    }
}
