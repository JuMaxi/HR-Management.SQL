using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    public class GateManagement
    {
        Dictionary<string, People> AddPeople = new Dictionary<string, People>();
        public ConnectionSQL ConnectionDB = new ConnectionSQL();

        public void AddVisitor(People NewPerson)
        {
            AddPeople.Add(NewPerson.NumberCPF.Number,NewPerson);
        }
        public void CheckAccess(string CPF)
        {
            if (AddPeople.ContainsKey(CPF))
            {
                Console.WriteLine("Access permitted to " + AddPeople[CPF].Name);
            }
            else
            {
                Console.WriteLine("Access denied to CPF number " + CPF);
            }
        }
    }
}
