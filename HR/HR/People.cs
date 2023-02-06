using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    public class People
    {
        public string Name;
        public CPF NumberCPF;

        public void InitializePeople(string NamePeople, string NuCPF)
        {
            CPF CallNumber = new CPF(NuCPF);

            Name = NamePeople;
            NumberCPF = CallNumber;
        }
    }
}
