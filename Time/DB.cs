using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Time
{
    public class DB
    {
        public static List<WeightNeckMtxPeriod> Table = new List<WeightNeckMtxPeriod>();
        

        public static void GetData()
        {

            Table.Clear();

            string connectionString = "Server=192.168.0.12;Database=Planning;Password=DbSyS@dm1n;User ID=sa";
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = "192.168.0.12";
            csb.InitialCatalog = "planning";
            csb.UserID = "sa";
            csb.Password = "DbSyS@dm1n";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                connection.Open();
                SqlCommand command = new SqlCommand("select * from Sets", connection);
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    

                    

                    while (reader.Read()) // построчно считываем данные
                    {
                        WeightNeckMtxPeriod p = new WeightNeckMtxPeriod();
                        p.WGTH = Convert.ToDouble(reader.GetValue(2));
                        p.NCK = reader.GetString(1);
                        p.MTX = Convert.ToInt32(reader.GetString(3));
                        Table.Add(p);


                    }
                }


            }
        }



        static int N = 120;
        public static List<double> RandomPeriod(int MachineN)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            List<double> t = new List<double>();
            for (int i = 0; i < N; i++)
            {

                t.Add(8 * rnd.NextDouble() + 10);

            }
            return t;
        }
        public static double RandomNominal(int MachineN)
        {            
            return Table[MachineN].Nominal_Cycle_Period;
        }

    }
    public class WeightNeckMtxPeriod
    {
        public double WGTH { get; set; }
        public string NCK { get; set; }
        public int MTX { get; set; }
        public double Nominal_Cycle_Period { get; set; }

    }
}
