using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Web;

namespace ciceksepeti.database
{
    public class Sqlconnection
    {
        public static SqlConnection connection =new SqlConnection("Data Source=DESKTOP-GSBLGK3\\SQLEXPRESS;Initial Database=ciceksepeti;Integrated Security=True;Trust Server Certificate=True");
        public static void CheckConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            else 
            { 
            }
        }
    }
}
