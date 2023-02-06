using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Spectre.Console.Rendering;
using System.Globalization;

namespace HR
{
    public class Menu
    {
        public void WriteNameCompany()
        {
            Console.WriteLine("+-------------------+");
            Console.WriteLine("Company Example HR");
            Console.WriteLine("+-------------------+");
        }
        public void WriteOptions()
        {
            WriteNameCompany();

            Console.WriteLine("Please, type the option you want, according the Options below: ");
            Console.WriteLine(" ");

            Console.WriteLine("1) Create Employee: ");
            Console.WriteLine("2) List Employee: ");
            Console.WriteLine("3) Show Birthday Company: ");
            Console.WriteLine("4) Find Oldest Employee: ");
            Console.WriteLine("5) Calculate Salary: ");
            Console.WriteLine("6) Dismiss Employee: ");
            Console.WriteLine("7) Promote Employee: ");
            Console.WriteLine("8) Union Agreement: ");
            Console.WriteLine("9) Calculate 13 Salary: ");
            Console.WriteLine("10) Rescisao: ");
            Console.WriteLine("11) Exit: ");
            Console.Write("--> ");
        }

        public void ExitMessage()
        {
            Console.WriteLine(" ");
            Console.Write("Type any key to return");
            Console.ReadKey();
            Console.Clear();
        }
       
        public void Options(EmployeeManagement AccessClassEM)
        {
            string Option = "";

            while (Option != "11")
            {
                WriteOptions();

                Option = Console.ReadLine();

                Console.Clear();

                if (Option == "1")
                {
                    Employee AccessClassE = new Employee();

                    WriteNameCompany();

                    Console.Write("Please, type the Complet Name: ");
                    AccessClassE.Name = Console.ReadLine();

                    Console.Write("Please, type the Number of Registry: ");
                    AccessClassE.Registry = Console.ReadLine();

                    Console.Write("Please, type the CPF: ");
                    string CPF = Console.ReadLine();

                    Console.Write("Please, type the Start Date (YYYY/MM/DD): ");
                    string DateString = Console.ReadLine();
                    AccessClassE.DateStart = Convert.ToDateTime(DateString);

                    Console.Write("Please, type the Salary: ");
                    string SalaryString = Console.ReadLine();
                    AccessClassE.MonthlySalary = Convert.ToDouble(SalaryString);

                    try
                    {
                        AccessClassE.Initialize(AccessClassE.Name, AccessClassE.Registry, CPF, AccessClassE.DateStart, AccessClassE.MonthlySalary);
                        AccessClassEM.AddEmployee(AccessClassE);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Something is wrong. Check the error message, please. " + ex.Message);
                        Console.WriteLine(" ");
                    }

                    ExitMessage();
                }
                if (Option == "2")
                {
                    WriteNameCompany();

                    AccessClassEM.ListEmployee();

                    ExitMessage();
                }
                if (Option == "3")
                {
                    WriteNameCompany();

                    AccessClassEM.ShowBirthdayCompany();

                    ExitMessage();
                }
                if (Option == "4")
                {
                    WriteNameCompany();

                    AccessClassEM.FindOldestEmployee();

                    ExitMessage();
                }
                if (Option == "5")
                {
                    WriteNameCompany();

                    Console.Write("Please, type the Date(YYYY/MM/DD): ");
                    string DateString = Console.ReadLine();
                    DateTime Date = Convert.ToDateTime(DateString);

                    AccessClassEM.CalculateSalary(Date);

                    ExitMessage();
                }
                if (Option == "6")
                {
                    WriteNameCompany();

                    Console.Write("Please, type the Employee's Number Registry: ");
                    string Registry = Console.ReadLine();

                    AccessClassEM.DismissEmployee(Registry);

                    ExitMessage();

                }
                if (Option == "7")
                {
                    WriteNameCompany();

                    Console.Write("Please, type the Employee's Number Registry: ");
                    string Registry = Console.ReadLine();

                    Console.Write("Please, type the promote's Percentage: ");
                    string PercentageString = Console.ReadLine();
                    double Percentage = Convert.ToDouble(PercentageString);

                    AccessClassEM.PromoteEmployee(Registry, Percentage);

                    Console.WriteLine(" ");
                    Console.WriteLine("The Promote was completed with successfull.");

                    ExitMessage();
                }
                if (Option == "8")
                {
                    WriteNameCompany();

                    Console.Write("Please, type the Union Agreement Percentage: ");
                    string PercentageString = Console.ReadLine();
                    double Percentage = Convert.ToDouble(PercentageString);

                    Console.Write("Please, type the Union Agreement Date (YYYY/MM/DD): ");
                    string DateString = Console.ReadLine();
                    DateTime DateAgreement = Convert.ToDateTime(DateString);


                    AccessClassEM.UnionAgreement(Percentage, DateAgreement);

                    Console.WriteLine(" ");
                    Console.WriteLine("The Union Agreement was completed with successfull.");

                    ExitMessage();
                }
                if (Option == "9")
                {
                    WriteNameCompany();

                    Console.Write("Please, type the Date (YYYY/MM/DD): ");
                    string DateString = Console.ReadLine();
                    DateTime Date = Convert.ToDateTime(DateString);

                    AccessClassEM.Calculate13Salary(Date);

                    ExitMessage();
                }
                if (Option == "10")
                {
                    WriteNameCompany();

                    Console.Write("Please, type the Employee' Number Registry: ");
                    string Registry = Console.ReadLine();

                    Console.Write("Please, type the Exit Date (YYYY/MM/DD): ");
                    string DateString = Console.ReadLine();
                    DateTime Date = Convert.ToDateTime(DateString);

                    AccessClassEM.Rescisao(Registry, Date);

                    ExitMessage();
                }
            }
        }
    }
}
