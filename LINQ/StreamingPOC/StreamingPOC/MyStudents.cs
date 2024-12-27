using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPOC
{
    internal class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    internal static class MyStudents
    {
       

       public static IEnumerable<Student> GetTeenagerStudentStreaming(this IEnumerable<Student> source)
       {    
            foreach(var item in source)
            {
                if(item.Age is > 12 and < 20)
                {
                    yield return item;
                }
            }
       }

       public static IEnumerable<Student> GetTeenagerStudentNonStreaming(this IEnumerable<Student> source)
       {
            IList<Student> teenagers = new List<Student>();
            foreach(var item in source)
            {
                if(item.Age is > 12 and < 20)
                {
                    teenagers.Add(item);
                }
            }
            return teenagers;
       }

    }
}
