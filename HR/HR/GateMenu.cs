using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    public class GateMenu
    {
        Menu CallMethods = new Menu();

        public void InitialChoice()
        {
            CallMethods.WriteNameCompany();

            Console.WriteLine("Please, type the option you need: ");
            Console.WriteLine(" ");
            Console.WriteLine("1) Add people: ");
            Console.WriteLine("2) Check authorized access: ");
            Console.WriteLine("3) Exit: ");
            Console.Write("--> ");
        }
        public void OptionGate(GateManagement AccessGM)
        {
            string Options = "0";

            while (Options != "3")
            {
                InitialChoice();

                Options = Console.ReadLine();
                Console.Clear();

                if(Options == "1")
                {
                    People AccessPeople= new People();

                    CallMethods.WriteNameCompany();

                    Console.Write("Please type the Person's Name: ");
                    AccessPeople.Name = Console.ReadLine();

                    Console.Write("Please, type the Number CPF: ");
                    string CPFNumber = Console.ReadLine();

                    try
                    {
                        AccessPeople.InitializePeople(AccessPeople.Name, CPFNumber);
                        AccessGM.AddVisitor(AccessPeople);
                    }
                    catch(Exception ex) 
                    {
                        Console.WriteLine("Something is wrong. Please, check the erro message " + ex.Message);
                    }

                    CallMethods.ExitMessage();
                }
                if(Options == "2")
                {
                    CallMethods.WriteNameCompany();

                    Console.Write("Please, type the Number CPF: ");
                    string CPFNumber = Console.ReadLine();
                    AccessGM.CheckAccess(CPFNumber);

                    CallMethods.ExitMessage();
                }
            }

        }
    }
}
