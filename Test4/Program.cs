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
        public static List<ConfigurationWithMachineN> TableWithOrdinals = new List<ConfigurationWithMachineN>();
        static void Main(string[] args)
        {
            TableWithOrdinals.Clear();
            string connectionString = "Server=192.168.0.12;Database=Planning;Password=DbSyS@dm1n;User ID=sa";
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = "192.168.0.12";
            csb.InitialCatalog = "planning";
            csb.UserID = "sa";
            csb.Password = "DbSyS@dm1n";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command2 = new SqlCommand())
                {
                    command2.CommandText = "SELECT DISTINCT Sets.neck,Sets.weight,Sets.matrix,planOrders.startDate,planOrders.endDate,planOrders.petLine FROM Sets LEFT JOIN planOrders ON (Sets.neck=planOrders.neck AND Sets.weight=planOrders.weight AND Sets.matrix=planOrders.matrix) WHERE GETDATE() BETWEEN startDate AND endDAte";
                    command2.Connection = connection;
                    SqlDataReader reader = command2.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            ConfigurationWithMachineN q = new ConfigurationWithMachineN();
                            q.WGTH = Convert.ToDouble(reader.GetValue(1));
                            q.NCK = reader.GetString(0);
                            q.MTX = Convert.ToInt32(reader.GetString(2));
                            q.startDate = reader.GetDateTime(3);
                            q.endDate = reader.GetDateTime(4);
                            q.ordinal = (reader.GetString(5).Substring(7).StartsWith("0")) ? Convert.ToInt32(reader.GetString(5).Substring(8)) : Convert.ToInt32(reader.GetString(5).Substring(7));
                            TableWithOrdinals.Add(q);
                        }
                    }
                }
            }

            foreach(ConfigurationWithMachineN it in TableWithOrdinals)
            {

                Console.WriteLine(it);
            }
            Console.ReadLine();
        }
    }
    
    
    public class ConfigurationWithMachineN
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public double WGTH { get; set; }
        public string NCK { get; set; }
        public int MTX { get; set; }
        public int ordinal { get; set; }
    }
}
