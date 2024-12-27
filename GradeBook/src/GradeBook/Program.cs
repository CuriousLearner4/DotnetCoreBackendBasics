namespace GradeBook
{
    class Program
    {
        static public void Main(String[] args)
        {
            IBook book = new DiskBook("harsha");

            Console.WriteLine("Welcome to Grade Book");

            while(true){
                Console.WriteLine("Select option:\n1.Enter grades\n2.View statistics\n3.exit");
                string ? input = Console.ReadLine();
                switch(input){
                    case "1":
                        Console.WriteLine("Enter grade or q to stop:");
                        EnterGrade(book);
                        break;
                    case "2":
                        viewStatistics(book);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Wrong input!!!.Please select 1 or 2 or 3 from above to continue");
                        break;
                }
            }
        }

        private static void viewStatistics(IBook book)
        {
            Statistics? s = book.getStatistics();
            if (s != null)
            {   
                Console.WriteLine($"Highest grade is {s?.HighestScore:N2}");
                Console.WriteLine($"Lowest grade is {s?.LowestScore:N2}");
                Console.WriteLine($"Average of grades is {s?.Average:N2}");
                Console.WriteLine($"Letter is {s?.Letter}");
            }else {
                Console.WriteLine("No grades in book");
            }
        }

        private static void EnterGrade(IBook book)
        {
            while (true)
            {
                string? input = Console.ReadLine();
                if (String.Equals(input?.ToUpper(), "Q")) break;
                try
                {
                    if (input != null)
                    {
                        double grade = double.Parse(input);
                        book.addGrade(grade);
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

    }
}