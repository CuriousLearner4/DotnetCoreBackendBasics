using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.OCP
{
    public abstract class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Employee() { }

        public Employee(int id, string name)
        {
            ID = id;
            Name = name;  
        }
        public abstract decimal CalculateBonus(decimal salary);
        public override string ToString()
        {
            return string.Format("ID:{0} Name: {1}", ID, Name);
        }
    }

    public class ParmanentEmployee : Employee
    {
        public ParmanentEmployee(int id, string name):base(id,name) { 
            
        }

        public override decimal CalculateBonus(decimal salary)
        {
            return salary*0.1M;
        }
    }

    public class TemporaryEmployee : Employee
    {
        public TemporaryEmployee(int id, string name) : base(id, name)
        {

        }

        public override decimal CalculateBonus(decimal salary)
        {
            return salary * 0.1M;
        }
    }

    public class ContractEmployee : Employee
    {
        public ContractEmployee(int id, string name) : base(id, name)
        {

        }

        public override decimal CalculateBonus(decimal salary)
        {
            return salary * 0.1M;
        }
    }


}
