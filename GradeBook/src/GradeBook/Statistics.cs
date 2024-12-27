
namespace GradeBook{

    public class Statistics{

        private int count;
        private double lowestScore;
        private double highestScore;

        private double average;

        private char letter;

        public int Count{
            get{
                return count;
            }
        }

        public double LowestScore{
            get{
                return lowestScore;
            }
        }

        public double HighestScore{
            get{
                return highestScore;
            }
        }

        public double Average{
            get{
                return average;
            }
        }
        
        public char Letter{
            get{
                return letter;
            }
        }
        public Statistics(){
            highestScore = double.MinValue;
            lowestScore = double.MaxValue;
            average = 0D;
        }

        public void computeOnAdd(double grade){
            computeHighestGrade(grade);
            computeLowestGrade(grade);
            computeAverage(grade);
            ComputeLetter();
            count++;
        }

        private void computeHighestGrade(double grade){
            highestScore = Math.Max(highestScore,grade);
        }

        private void computeLowestGrade(double grade){
            lowestScore = Math.Min(lowestScore,grade);
        }

        private void computeAverage(double grade){
            average = ((average*count)+grade)/(count+1);
        }

        public void ComputeLetter(){
            switch(Average){
                case var d when d>=90:
                    letter = 'A';
                    break;
                
                case var d when d>=80:
                    letter = 'B';
                    break;
                case var d when d>=70:
                    letter = 'C';
                    break;
                case var d when d>=60:
                    letter = 'D';
                    break;
                case var d when d>=50:
                    letter = 'E';
                    break;
                case var d when d>=40:
                    letter = 'P';
                    break;
                default:
                    letter = 'F';
                    break;
            }
        }

    }
}