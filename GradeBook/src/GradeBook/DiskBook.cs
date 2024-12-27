using System.Xml.XPath;

namespace GradeBook{
    public class DiskBook : Book
    {
        Statistics s1;
        public DiskBook(string name) : base(name)
        {
            s1 = new Statistics();
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void addGrade(double grade)
        {
            using(var writer = File.AppendText($"{Name}.txt")){
                writer.WriteLine(grade);
                if(GradeAdded!=null){
                    GradeAdded(this,new EventArgs());
                }
            }
            s1.computeOnAdd(grade);
        }

        public override Statistics getStatistics()
        {
            var result = new Statistics();
            using(var reader = File.OpenText($"{Name}.txt")){
                var line = reader.ReadLine();
                while(line!=null){
                    var number = double.Parse(line);
                    result.computeOnAdd(number);
                    line = reader.ReadLine();
                }
            }
            return result.Count==0?null:result;
        }
    }
}