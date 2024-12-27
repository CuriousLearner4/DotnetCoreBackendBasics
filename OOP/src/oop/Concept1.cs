 //A delegate is a type safe function pointer

delegate bool IsPromotable(Employee e);
class Employee{
    //auto implemented getter and setter methods
    //Properties in c#
    public int id{ get; set;} 
    public string name{get;set;}
    public int salary{get;set;}
    public int experience {get;set;}
    
    //collection checkout 
    public static void promotedEmployee(List<Employee> employees,IsPromotable isPromotable){
        foreach(Employee employee in employees ){
            if(isPromotable(employee)){
                Console.WriteLine(employee.name); 
            }
        }
    }
}