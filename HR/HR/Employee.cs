using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    public class Employee
    {
        public string Name;
        public string Registry;
        public CPF NumberEmployee;
        public DateTime DateStart;
        public double MonthlySalary;
        public void Initialize(string NameEmployee, string NumberRegistry, string NumberCPF, DateTime Date, double Salary)
        {
            CPF CallCPF = new CPF(NumberCPF);

            Name = NameEmployee;
            Registry = NumberRegistry;
            NumberEmployee = CallCPF;
            DateStart = Date;
            MonthlySalary = Salary;
        }
        public void Validate()
        {
            string NameTrim = Name.Trim();
            Name = NameTrim;

            if (Name.IndexOf(" ") < 0)
            {
                throw new Exception("The name is not completed. You must fill this field with the Complet Name.");
            }

            if (Name == " ")
            {
                throw new Exception("The Name is null. You must fill this field.");
            }
            if (Registry == " ")
            {
                throw new Exception("The Registry is null. For continue, you must fill this field.");
            }

            if (MonthlySalary < 0)
            {
                throw new Exception("The Monthly Salary is null. For continue, you must fill this field with a value bigger than zero.");
            }

        }
        
    }
}

