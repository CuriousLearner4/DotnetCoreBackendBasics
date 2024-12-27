
namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender,EventArgs args);
    public class InMemoryBook:Book
    {
        private List<double> grades;

        // public string Name{
        //     get{
        //         return name;
        //     }
        //     set{
        //         name = value;
        //     }
        // }
        public Statistics s1;
        public override event GradeAddedDelegate GradeAdded;

        public InMemoryBook(string name):base(name){
            grades = new List<double>();
            s1 = new Statistics();
            this.GradeAdded += onGradeAdded;
        }
        
        
        public override void addGrade(double grade){
            if(grade>=0&&grade<=100)
            {
                grades.Add(grade);
                if(GradeAdded!=null){
                    GradeAdded(this,new EventArgs());
                }
                s1.computeOnAdd(grade);
            
            }else{
                throw new ArgumentException("Invalid Grade");
            }
        }

        public override Statistics? getStatistics(){
            /*complexity of show statistics can be optimized
            as I am going through the tuttorial I am following the same
            Current complexity O(N) where N is the length of grades*/
            // var highestGrade = double.MinValue;
            // var lowestGrade = double.MaxValue;
            // var average = 0D;
            // foreach(double grade in grades){
            //     average += grade;
            //     lowestGrade = Math.Min(lowestGrade,grade); //using functions static methods of Mathclass
            //     highestGrade = Math.Max(highestGrade,grade);
            // }
            // average/=grades.Count;
            // Console.WriteLine($"Highest grade is {highestGrade:N2}");
            // Console.WriteLine($"Lowest grade is {lowestGrade:N2}");
            // Console.WriteLine($"Average of grades is {average:N2}");
            return s1.Count==0?null:s1;
        }
         public static void onGradeAdded(object o,EventArgs e){
            Console.WriteLine("Grade added");
        }
    }
}