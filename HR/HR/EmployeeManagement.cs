using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace HR
{
    public class EmployeeManagement
    {
        public Calculate Calculate = new Calculate();
        public ConnectionSQL ConnectionDB = new ConnectionSQL();

        public void AddEmployee(Employee NewEmployee)
        {
            NewEmployee.Validate();

            string Insert = "insert into Employee (Name, Registry, CPF, DateStart, MonthlySalary, Dismissed) values ('" + NewEmployee.Name + "','" + NewEmployee.Registry + "','" + NewEmployee.NumberEmployee.Number + "','" + NewEmployee.DateStart.ToString("yyyyMMdd") + "','" + NewEmployee.MonthlySalary + "','False')";

            ConnectionDB.AccessNonQuery(Insert);

        }

        public void ListEmployee()
        {
            string Select = "select * from Employee";

            SqlDataReader Reader = ConnectionDB.AccessReader(Select);

            while (Reader.Read())
            {
                Console.WriteLine(" ");
                Console.WriteLine("ID: " + Reader["Id"]);
                Console.WriteLine("Employee Name: " + Reader["Name"]);
                Console.WriteLine("Registry: " + Reader["Registry"]);
                Console.WriteLine("CPF: " + Reader["CPF"]);
                Console.WriteLine("Date Start: " + Convert.ToDateTime(Reader["DateStart"]).ToString("yyyy/MM/dd"));
                Console.WriteLine("Monthly Salary: " + Convert.ToDouble(Reader["MonthlySalary"]).ToString("C2"));
                Console.WriteLine("Dismissed Employee: " + Reader["Dismissed"]);
            }
        }

        public void ShowBirthdayCompany()
        {
            string Select = "select * from Employee";

            SqlDataReader Reader = ConnectionDB.AccessReader(Select);

            while (Reader.Read())
            {
                DateTime DateDB = Convert.ToDateTime(Reader["DateStart"]);
                TimeSpan Difference = DateTime.Now - DateDB;

                if (Difference.Days >= 365)
                {
                    if (DateDB.Month == DateTime.Now.Month
                        && DateDB.Day <= DateTime.Now.AddDays(15).Day)
                    {
                        Console.WriteLine("Congratulations!!! " + Reader["Name"] + " Now you are having 1 year with us! We hope to have a lot more years together!");

                    }
                }
            }
        }

        internal void FindOldestEmployee()
        {
            string Select = "select * from Employee";
            SqlDataReader Reader = ConnectionDB.AccessReader(Select);

            DateTime Oldest = DateTime.Now;
            string Name = "";

            while (Reader.Read())
            {
                DateTime DateDB = Convert.ToDateTime(Reader["DateStart"]);

                if (DateDB < Oldest)
                {
                    Oldest = DateDB;
                    Name = Convert.ToString(Reader["Name"]);
                }
            }
            Console.WriteLine(Name + " you are the oldest employee in this company. You have been with us since " + Oldest.ToString("yyyy/MM/dd") + ". We Hope you continue with us for a long time!");

        }
        public void PromoteEmployee(string Registry, double Percentage)
        {
            string Update = "update Employee set MonthlySalary= MonthlySalary * (1 + " + Percentage + ") where Registry='" + Registry + "'";
            ConnectionDB.AccessNonQuery(Update);
        }

        public void UnionAgreement(double Percentage, DateTime DateAgreement)
        {
            string Select = "select * from Employee where DateStart <='" + DateAgreement.ToString("yyyy/MM/dd") + "'";
            SqlDataReader Reader = ConnectionDB.AccessReader(Select);

            while (Reader.Read())
            {
                DateTime DateStart = Convert.ToDateTime(Reader["DateStart"]);

                if (DateStart.Year == DateAgreement.Year)
                {
                    TimeSpan DaysTotal = DateAgreement - DateStart;
                    double PercentageProportional = (Percentage / 365) * ((DaysTotal.Days % 365) + 1);

                    PromoteEmployee(Convert.ToString(Reader["Registry"]), PercentageProportional);
                }
                else
                {
                    PromoteEmployee(Convert.ToString(Reader["Registry"]), Percentage);
                }
            }
        }

        public void DismissEmployee(string Registry)
        {
            string Update = "update Employee set Dismissed='True' where Registry=" + Registry;

            ConnectionDB.AccessNonQuery(Update);

            Console.WriteLine("The Employee was dismiss");
        }

        public void CalculateSalary(DateTime Competencia)
        {
            Calculate.CalculateSalary(Competencia);
        }

        public void Calculate13Salary(DateTime Year13)
        {
            Calculate.Calculate13Salary(Year13);
        }

        public void Rescisao(string NumberRegistry, DateTime DateExit)
        {
            Calculate.Rescisao(NumberRegistry, DateExit);
        }
    }
}



