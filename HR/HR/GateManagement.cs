using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    public class GateManagement
    {
        public ConnectionSQL ConnectionDB = new ConnectionSQL();

        public void AddVisitor(People NewPerson)
        {
            string Insert = "insert into GateManagement(Name, CPF) values ('" + NewPerson.Name + "','" + NewPerson.NumberCPF.Number + "')";

            ConnectionDB.AccessNonQuery(Insert);
        }
        public void CheckAccess(string CPF)
        {
            string Select = "select * from GateManagement where CPF='" + CPF + "'";

            SqlDataReader Reader = ConnectionDB.AccessReader(Select);

            while(Reader.Read())
            {
                string StringCPF = Convert.ToString(Reader["CPF"]);

                if (StringCPF == CPF)
                {
                    Console.WriteLine("Access permitted to " + Reader["Name"]);
                }
                else
                {
                    Console.WriteLine("Access denied to CPF number " + CPF);
                }
            }
        }
    }
}
