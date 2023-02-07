using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    public class Calculate
    {
        public ConnectionSQL ConnectionDB = new ConnectionSQL();
        public void CalculateTax(double Salary, SqlDataReader Reader)
        {
            if (Salary > 0)
            {
                double INSS = Salary * 0.07;
                double IRRF = (Salary - INSS) * 0.15;

                Console.WriteLine(" ");
                Console.WriteLine("Hello, " + Reader["Name"] + ", Number Registry " + Reader["Registry"] + " here are your salary details: ");
                Console.WriteLine("Monthly Salary: " + (Salary).ToString("C2"));
                Console.WriteLine("Date Start in this Company: " + Convert.ToDateTime(Reader["DateStart"]).ToString("yyyy/MM/dd"));
                Console.WriteLine("INSS: " + (-INSS).ToString("C2"));
                Console.WriteLine("IRRF: " + (-IRRF).ToString("C2"));
                Console.WriteLine("Liquid Salary: " + (Salary - INSS - IRRF).ToString("C2"));
                Console.WriteLine(" ");
            }
        }

        public void CalculateSalary(DateTime Competencia)
        {
            double Salary = 0;

            string Select = "select * from Employee where Dismissed = 'false'";

            SqlDataReader Reader = ConnectionDB.AccessReader(Select);

            while (Reader.Read())
            {
                DateTime DateStart = Convert.ToDateTime(Reader["DateStart"]);

                if (Competencia > DateStart)
                {
                    Salary = Convert.ToDouble(Reader["MonthlySalary"]);

                    if (Competencia.Year == DateStart.Year
                        && Competencia.Month == DateStart.Month)
                    {
                        int DaysMonth = DateTime.DaysInMonth(Competencia.Year, Competencia.Month);

                        Salary = (Salary / DaysMonth) * ((DaysMonth - DateStart.Day) + 1);
                    }
                }

                CalculateTax(Salary, Reader);
            }
        }

        public void Calculate13Salary(DateTime Year13)
        {
            double Salary = 0;

            string Select = "select * from Employee where Dismissed = 'false'";

            SqlDataReader Reader = ConnectionDB.AccessReader(Select);

            while (Reader.Read())
            {
                DateTime DateStart = Convert.ToDateTime(Reader["DateStart"]);

                if (Year13 > DateStart)
                {
                    Salary = Convert.ToDouble(Reader["MonthlySalary"]);

                    if (Year13.Year == DateStart.Year)
                    {
                        Salary = CalculateSalaryProportional(Reader, Year13);
                    }
                    CalculateTax(Salary, Reader);
                }
            }
        }

        public double CalculateSalaryProportional(SqlDataReader Reader, DateTime Date)
        {
            DateTime DateStart = Convert.ToDateTime(Reader["DateStart"]);
            TimeSpan DaysTotal = Date - DateStart;

            double Salary = ((Convert.ToDouble(Reader["MonthlySalary"])) / 365) * ((DaysTotal.Days % 365) + 1);

            if (DateStart.Day > 15)
            {
                int DaysMonthStart = DateTime.DaysInMonth(DateStart.Year, DateStart.Month);
                Salary = (Salary / 365) * (((DaysTotal.Days % 365) + 1) - DaysMonthStart);

                if (Salary < 0)
                {
                    Salary = 0;
                }
            }
            return Salary;
        }

        public void Rescisao(string NumberRegistry, DateTime DateExit)
        {
            string Select = "select * from Employee where Registry=" + NumberRegistry;

            SqlDataReader Reader = ConnectionDB.AccessReader(Select);

            while (Reader.Read())
            {
                if (Convert.ToString(Reader["Dismissed"]) == "True")
                {
                    double SalaryRescisao = 0;
                    double HolidaysRescisao = CalculateSalaryProportional(Reader, DateExit);

                    if (DateExit.Day > 15)
                    {
                        SalaryRescisao = Convert.ToDouble(Reader["MonthlySalary"]);
                    }
                    Console.WriteLine("");
                    Console.WriteLine("Rescisao Calculation");
                    CalculateTax((HolidaysRescisao + SalaryRescisao + (HolidaysRescisao * 0.33)), Reader);
                }
                else
                {
                    Console.WriteLine("This registry is not from a Dismissed Employee. If you want to dismiss it, you must first to choose the option Dismiss Employee and then return to calculate the Rescisao.");
                }
            }

        }
    }
}
