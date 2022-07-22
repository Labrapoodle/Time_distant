using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Test4
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string connectionString = "Server=192.168.0.12;Database=PetPro;Password=DbSyS@dm1n;User ID=sa";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "INSERT INTO Time_Of_Changing_Orders([date],MachineN)VALUES('22-07-22T00:00:00.000',15)";
                    command.Connection = connection;
                    
                    command.ExecuteNonQuery();
                }
            }
        }
    }
    
    
    
}
