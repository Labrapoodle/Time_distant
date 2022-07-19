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
                for (int j = 1; j < 119; j++)
                {
                    Random rnd = new Random();
                    for (int k = 2; k < 15; k++)
                    {
                        using (SqlCommand command2 = new SqlCommand("AddCycleFromPhotometer"))
                        {

                            command2.Connection = connection;
                            command2.CommandType = System.Data.CommandType.StoredProcedure;
                            command2.Parameters.Add("@MachineNumber", System.Data.SqlDbType.Int).Value=k;
                            command2.Parameters.Add("@Period", System.Data.SqlDbType.Float).Value = 8*rnd.NextDouble()+10;
                            command2.ExecuteNonQuery();


                        }
                    }
                }
            }

            
            Console.ReadLine();
        }
    }
    
    
    
}
