using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    public class Calculate
    {
        public void CalculateTax(double Salary, Employee Line)
        {
            if (Salary > 0)
            {
                double INSS = Salary * 0.07;
                double IRRF = (Salary - INSS) * 0.15;

                Console.WriteLine(" ");
                Console.WriteLine("Hello, " + Line.Name + ", Number Registry " + Line.Registry + " here are your salary details: ");
                Console.WriteLine("Monthly Salary: " + (Salary).ToString("C2"));
                Console.WriteLine("Date Start in this Company: " + Line.DateStart.ToString("yyyy/MM/dd"));
                Console.WriteLine("INSS: " + (-INSS).ToString("C2"));
                Console.WriteLine("IRRF: " + (-IRRF).ToString("C2"));
                Console.WriteLine("Liquid Salary: " + (Salary - INSS - IRRF).ToString("C2"));
                Console.WriteLine(" ");
            }
        }

        public void CalculateSalary(DateTime Competencia, List<Employee> Employees)
        {

            for (int Position = 0; Position < Employees.Count; Position++)
            {
                Employee Line = Employees[Position];
                double Salary = Line.MonthlySalary;

                if (Competencia > Line.DateStart)
                {
                    if (Competencia.Year == Line.DateStart.Year)
                    {
                        if (Competencia.Month == Line.DateStart.Month)
                        {
                            int DaysMonth = DateTime.DaysInMonth(Competencia.Year, Competencia.Month);
                            Salary = (Line.MonthlySalary / DaysMonth) * ((DaysMonth - Line.DateStart.Day) + 1);
                        }
                    }
                }
                CalculateTax(Salary, Line);
            }
        }
        public void Calculate13Salary(DateTime Year13, List<Employee> Employees)
        {
            for (int Position = 0; Position < Employees.Count; Position++)
            {
                double Salary = 0;
                if (Year13 > Employees[Position].DateStart)
                {
                    if (Year13.Year == Employees[Position].DateStart.Year)
                    {
                        Salary = CalculateSalaryProportional(Employees[Position], Year13);
                    }
                    else
                    {
                        Salary = Employees[Position].MonthlySalary;
                    }

                }
                CalculateTax(Salary, Employees[Position]);
            }
        }
        public double CalculateSalaryProportional(Employee EmployeeProportional, DateTime Date)
        {
            TimeSpan DaysTotal = Date - EmployeeProportional.DateStart;
            int DaysActualYear = (DaysTotal.Days % 365) + 1;
            double Salary = 0;

            if (DaysTotal.Days < 365 && EmployeeProportional.DateStart.Day > 15)
            {
                int DaysMonthStart = DateTime.DaysInMonth(EmployeeProportional.DateStart.Year, EmployeeProportional.DateStart.Month);
                Salary = ((EmployeeProportional.MonthlySalary / 365) * (DaysActualYear - DaysMonthStart));

                if (Salary < 0)
                {
                    Salary = 0;
                }
            }
            else
            {
                Salary = (EmployeeProportional.MonthlySalary / 365) * DaysActualYear;
            }
            return Salary;
        }

        public void Rescisao(string NumberRegistry, DateTime DateExit, List<Employee> Removed)
        {
            //PROPORCIONAL CALCULO FERIAS;

            foreach (Employee EmployeeRescisao in Removed)
            {
                if (NumberRegistry == EmployeeRescisao.Registry)
                {
                    double SalaryRescisao = 0;
                    double HolidaysRescisao = CalculateSalaryProportional(EmployeeRescisao, DateExit);

                    if (DateExit.Day > 15)
                    {
                        SalaryRescisao = EmployeeRescisao.MonthlySalary;
                    }
                    Console.WriteLine("");
                    Console.WriteLine("Rescisao Calculation");
                    CalculateTax((HolidaysRescisao + SalaryRescisao + (HolidaysRescisao * 0.33)), EmployeeRescisao);
                }
            }
        }
    }
}
