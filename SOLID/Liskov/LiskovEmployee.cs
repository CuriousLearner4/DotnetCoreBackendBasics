using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.Liskov
{
   
    public abstract class LiskovEmployee:IEmployee
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public LiskovEmployee() { }

        public LiskovEmployee(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public override string ToString()
        {
            return string.Format("ID:{0} Name: {1}", ID, Name);
        }

        public abstract decimal GetMinimumSalary();
    }

    public class ParmanentEmployee : LiskovEmployee,IEmployeeBonus
    {
        public ParmanentEmployee(int id, string name) : base(id, name)
        {

        }


        public decimal CalculateBonus(decimal salary)
        {
            return salary*.1M;
        }

        public override decimal GetMinimumSalary()
        {
           return 15000;
        }
    }

    public class TemporaryEmployee : LiskovEmployee,IEmployeeBonus
    {
        public TemporaryEmployee(int id, string name) : base(id, name)
        {

        }

        public decimal CalculateBonus(decimal salary)
        {
            return salary*0.5M;
        }

        public override decimal GetMinimumSalary()
        {
            return 10000;
        }
    }

    public class ContractEmployee : LiskovEmployee
    {
        public ContractEmployee(int id, string name) : base(id, name)
        {

        }


        public override decimal GetMinimumSalary()
        {
            return 5000;
        }
    }
}
