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
        public List<Employee> NewHiredEmployee = new List<Employee>();
        public List<Employee> RemovedEmployee = new List<Employee>();
        public Calculate Calculate = new Calculate();

        string ConnectionString = "Server=LAPTOP-P4GEIO8K\\SQLEXPRESS;Database=HR;User Id=sa;Password=S4root;";

        public void AddEmployee(Employee NewEmployee)
        {
            NewEmployee.Validate();

           using(SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                string insert = "insert into Employee (Name, Registry, CPF, DateStart, MonthlySalary) values ('" + NewEmployee.Name + "','" + NewEmployee.Registry + "','" + NewEmployee.NumberEmployee.Number + "','" + NewEmployee.DateStart.ToString("yyyyMMdd") + "','" + NewEmployee.MonthlySalary + "')";
                SqlCommand Command = new SqlCommand(insert, Connection);

                Connection.Open();

                Command.ExecuteNonQuery();
            }
        }

        public void ListEmployee()
        {
           using(SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                string select = "select * from Employee";

                SqlCommand command = new SqlCommand(select, Connection);

                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("ID: " + reader["Id"]);
                    Console.WriteLine("Employee Name: " + reader["Name"]);
                    Console.WriteLine("Registry: " + reader["Registry"]);
                    Console.WriteLine("CPF: " + reader["CPF"]);
                    Console.WriteLine("Date Start: " + Convert.ToDateTime(reader["DateStart"]).ToString("yyyy/MM/dd") );
                    Console.WriteLine("Monthly Salary: " + reader["MonthlySalary"]);
                }
            }
        }

        public void ShowBirthdayCompany()
        {
            using(SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                string select = "select * from Employee";

                SqlCommand Command = new SqlCommand(select, Connection);

                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                while(Reader.Read())
                {
                    DateTime DateDB = Convert.ToDateTime(Reader["DateStart"]);
                    TimeSpan Difference = DateTime.Now - DateDB;

                    if (Difference.Days >= 365)
                    {
                        if(DateDB.Month == DateTime.Now.Month
                            && DateDB.Day <= DateTime.Now.AddDays(15).Day)
                        {
                            Console.WriteLine("Congratulations!!! " + Reader["Name"] + " Now you are having 1 year with us! We hope to have a lot more years together!");

                        }
                    }
                }
            }
        }

        internal void FindOldestEmployee()
        {
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                string Select = "select * from Employee";

                SqlCommand Command = new SqlCommand(Select, Connection);

                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                DateTime Oldest = DateTime.Now;
                string Name = "";

                while (Reader.Read())
                {
                    DateTime DateDB = Convert.ToDateTime(Reader["DateStart"]);

                    if(DateDB < Oldest)
                    {
                        Oldest = DateDB;
                        Name = Convert.ToString(Reader["Name"]);
                    }
                }
                Console.WriteLine(Name + " you are the oldest employee in this company. You have been with us since " + Oldest.ToString("yyyy/MM/dd") + ". We Hope you continue with us for a long time!");
            }
        }
        public void PromoteEmployee(string Registry, double Percentage)
        {
            using(SqlConnection Connection = new SqlConnection(ConnectionString))
            {
               string Update = "update Employee set MonthlySalary= MonthlySalary * (1 + " + Percentage + ") where Registry='" + Registry + "'";

                SqlCommand Command = new SqlCommand(Update, Connection);

                Connection.Open();

                Command.ExecuteNonQuery();
            }
        }

        public void UnionAgreement(double Percentage, DateTime DateAgreement)
        {
            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                Employee Proportional = NewHiredEmployee[Position];
                TimeSpan DaysTotal = DateAgreement - Proportional.DateStart;

                if (DaysTotal.Days > 0)
                {
                    if (DaysTotal.Days < 365)
                    {
                        int DaysActualYear = (DaysTotal.Days % 365) + 1;

                        double PercentageProportional = (Percentage / 365) * DaysActualYear;

                        PromoteEmployee(NewHiredEmployee[Position].Registry, PercentageProportional);
                    }
                    else
                    {
                        PromoteEmployee(NewHiredEmployee[Position].Registry, Percentage);
                    }
                }
            }
        }

        public void DismissEmployee(string Registry)
        {
            Employee EmployeeRescisao = CheckRegistry(Registry);

            RemovedEmployee.Add(EmployeeRescisao);

            NewHiredEmployee.Remove(EmployeeRescisao);

            Console.WriteLine("The Employee " + EmployeeRescisao.Name + " Registry Number: " + EmployeeRescisao.Registry + " was Dismiss.");

        }

        public void CalculateSalary(DateTime Competencia)
        {
            Calculate.CalculateSalary(Competencia, NewHiredEmployee);
        }

        public Employee CheckRegistry(string Registry)
        {
            for (int Position = 0; Position < NewHiredEmployee.Count; Position++)
            {
                if (NewHiredEmployee[Position].Registry == Registry)
                {
                    return NewHiredEmployee[Position];
                }
            }
            return null;
        }

        public void Calculate13Salary(DateTime Year13)
        {
            Calculate.Calculate13Salary(Year13, NewHiredEmployee);
        }

        public void Rescisao(string NumberRegistry, DateTime DateExit)
        {
            Calculate.Rescisao(NumberRegistry, DateExit, RemovedEmployee);
        }
    }
}
