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
                    SqlCommand command = new SqlCommand("select * from Sets", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine("Подключение открыто\n");
                    if (reader.HasRows)
                    {
                        string columnName1 = reader.GetName(2) ;
                        string columnName2 = reader.GetName(1);
                        string columnName3 = reader.GetName(3);

                        Console.WriteLine($"{columnName1}\t\t{columnName2}\t\t{columnName3}\n");
                        while (reader.Read()) // построчно считываем данные
                        {
                            double Weigth = Convert.ToDouble(reader.GetString(2));
                            string Neck = reader.GetString(1);
                            int Matrix = Int32.Parse(reader.GetString(3));

                            Console.WriteLine($"{Weigth} \t\t{Neck} \t\t{Matrix}");
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
