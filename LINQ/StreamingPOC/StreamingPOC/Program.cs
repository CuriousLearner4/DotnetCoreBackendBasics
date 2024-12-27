namespace StreamingPOC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IList<Student> studentList = new List<Student>() {
            new Student() { Name="zvd",Age=16},
            new Student() { Name="kvd",Age=4},
            new Student() { Name="cvd",Age=1},
            new Student() { Name="rvd",Age=12},
            new Student() { Name="rvd",Age=13},
            new Student() { Name="rvd",Age=15}
            };
            var teenagers = studentList.GetTeenagerStudentNonStreaming().Take(2);
            foreach(var teenager in teenagers)
            {
                Console.WriteLine(teenager.Name);
            }

        }
    }
}
