using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Test3
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=192.168.0.12;Database=Planning;Password=DbSyS@dm1n;User ID=sa";
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = "192.168.0.12";
            csb.InitialCatalog = "planning";
            csb.UserID = "sa";
            csb.Password = "DbSyS@dm1n";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("select * from planorders", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine("Подключение открыто");
                    if (reader.HasRows)
                    {
                        string columnName1 = reader.GetName(0);
                        string columnName2 = reader.GetName(1);
                        string columnName3 = reader.GetName(2);

                        Console.WriteLine($"{columnName1}\t{columnName2}\t{columnName3}");
                        while (reader.Read()) // построчно считываем данные
                        {
                            object petLineN = reader.GetValue(0);
                            object OrderId = reader.GetValue(2);
                            object SubOrderId = reader.GetValue(1);

                            Console.WriteLine($"{petLineN} \t{OrderId} \t{SubOrderId}");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {

                    Console.WriteLine("Завершение работы");
                    Console.Read();
                }
            }
        }
    }
}
