using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    public class SelectionKindOfManagement
    {
        Menu CallMessagesMenu = new Menu();

        public void InitialOptionsMenu()
        {
            CallMessagesMenu.WriteNameCompany();

            Console.WriteLine("Please, select the option you want: ");
            Console.WriteLine(" ");
            Console.WriteLine("1) Menu Gate: ");
            Console.WriteLine("2) Menu Management Employee: ");
            Console.WriteLine("3) Exit: ");
            Console.Write("--> ");
        }
        public void ChooseKindMenu()
        {
            string Options = "0";

            while (Options != "3")
            {
                InitialOptionsMenu();

                Options = Console.ReadLine();
                Console.Clear();

                if(Options == "1")
                {
                    GateManagement AccessGM = new GateManagement();

                    GateMenu StartGateMenu = new GateMenu();

                    StartGateMenu.OptionGate(AccessGM);
                }
                if(Options == "2")
                {
                    EmployeeManagement AccessEM = new EmployeeManagement();

                    Menu StartMenu = new Menu();

                    StartMenu.Options(AccessEM);
                }
            }
        }
    }
}
