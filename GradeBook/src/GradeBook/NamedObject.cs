 namespace GradeBook{

    public class NamedObject{

        public string Name{
            get;
            set;
        }

        public NamedObject(string name){
            Name= name;
        }
    }

    public interface IBook{
        void addGrade(double grade);
        Statistics getStatistics();

        string Name {get;}

        event GradeAddedDelegate GradeAdded;

    }

    public abstract class Book : NamedObject,IBook{
        public Book(string name) : base(name)
        {
        }
        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void addGrade(double grade);
        public abstract Statistics getStatistics();
        
    }

 }