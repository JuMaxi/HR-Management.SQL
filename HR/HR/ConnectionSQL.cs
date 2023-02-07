using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR
{
    public class ConnectionSQL
    {
        public string ConnectionString = "Server=LAPTOP-P4GEIO8K\\SQLEXPRESS;Database=HR;User Id=sa;Password=S4root;";

        public void AccessNonQuery(string Text)
        {
            using (SqlConnection Connection = new SqlConnection(ConnectionString))
            {
                SqlCommand Command = new SqlCommand(Text, Connection);

                Connection.Open();

                Command.ExecuteNonQuery();
            }
        }

        public SqlDataReader AccessReader(string TextRead)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);

            SqlCommand Command = new SqlCommand(TextRead, Connection);
            Connection.Open();

            SqlDataReader Reader = Command.ExecuteReader();
            return Reader;
        }





    }
}
