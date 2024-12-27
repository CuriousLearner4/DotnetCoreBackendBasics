// See https://aka.ms/new-console-template for more information


namespace OOP{

//polymorphism - method overiding 

class BaseClass{
    public virtual void Print(){
        Console.WriteLine("from base class");

    }

    public void Print1(){
        Console.WriteLine("from base class - method hiding");
    }
}


class DerivedClass: BaseClass{

    public override void Print(){
        Console.WriteLine("from derived class");
    }

    public new void Print1(){
        Console.WriteLine("from derived class - method hiding");
    }
}
class DerivedClass1 : DerivedClass{
    
}

class DerivedClass2 :BaseClass{
    public override void Print(){
        Console.WriteLine("from derived class - hierarchical inheritance");
    }

}
class Customer
{
    string _firstName;
    string _lastName;
    //constructors
    public Customer(): this("No first name provided","No last name provided"){

    }
    public Customer(string firstname,string lastName){
        _firstName = firstname;
        _lastName = lastName; 
    }
    
    public void printFullName(){
        Console.WriteLine(_firstName + _lastName);
    }

    ~Customer(){
        Console.WriteLine("Instance deleted");
    }
}

delegate int SimpleDelegate();
class Program{

    public static void Main(string[] args){
        //classes and objects
        Customer c1 = new Customer("harsha","gopisetti");
        c1.printFullName();
        Customer c2 = new Customer();
        c2.printFullName();

        //inheritance 
        //single inheritance
        DerivedClass d = new DerivedClass();
        d.Print1();
        //Multilevel inheritance
        Console.WriteLine("Multilevel Inheritance:");
        DerivedClass1 d1 = new DerivedClass1();
        //heirarical inheritance
        Console.WriteLine("hierarical Inheritance:");
        DerivedClass2 d2 = new DerivedClass2();
        d2.Print();
        //Polymorphism
        BaseClass b = new DerivedClass();
        b.Print();
        //method overloading vs method overriding
        //if we access hidden method from base class the base class method is invoked
        BaseClass b1 = new DerivedClass();
        b1.Print1();


        List<Employee> emplist = new List<Employee>();
        emplist.Add(new Employee(){id = 101, name = "harsha" , salary = 100000, experience = 5});
        emplist.Add(new Employee(){id = 102, name = "Vardhan" , salary = 10000, experience = 6});
        emplist.Add(new Employee(){id = 103, name = "Kumar" , salary = 1000000, experience = 8});
        emplist.Add(new Employee(){id = 104, name = "Gopi" , salary = 100000, experience = 4});
        // method 1 - using instance of a delegate
        IsPromotable isPromoted = new IsPromotable(promote);
        Employee.promotedEmployee(emplist,isPromoted);
        // method 2 - using lamda expression
        Employee.promotedEmployee(emplist,emp => emp.experience>=5);
        //multicast delegate
        //method 1
        SimpleDelegate del1 = new SimpleDelegate(simpleMethod1);
        SimpleDelegate del2 = new SimpleDelegate(simpleMethod2);
        SimpleDelegate del3;
        del3 = del1+del2;
        Console.WriteLine(del3());
        //method 2
        SimpleDelegate simpleDelegate = new SimpleDelegate(simpleMethod1);
        simpleDelegate+= simpleMethod2;
        Console.WriteLine(simpleDelegate());
        
    }
    public static bool promote(Employee e){
        if(e.experience>5) return true;
        return false;
    }

    public static int simpleMethod1(){
        return 1;
    }

    public static int simpleMethod2(){
        return 2;
    }

}


}
