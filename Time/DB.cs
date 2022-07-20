using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Time
{
    public class DB
    {
        public static List<Configuration> Table = new List<Configuration>();
        public static List<ConfigurationWithMachineN> TableWithOrdinals = new List<ConfigurationWithMachineN>();
        static int N = 120;

        public static void GetListOfConfigurations()
        {

            //Table.Clear();



            string connectionString = "Server=192.168.0.12;Database=PetPro;Password=DbSyS@dm1n;User ID=sa";
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = "192.168.0.12";
            csb.InitialCatalog = "planning";
            csb.UserID = "sa";
            csb.Password = "DbSyS@dm1n";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                connection.Open();
                using (SqlCommand command1 = new SqlCommand())
                {
                    command1.CommandText = "SELECT * FROM Configuration_Nominal";
                    command1.Connection = connection;
                    SqlDataReader reader = command1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            Configuration p = new Configuration();
                            p.WGHT = Convert.ToDouble(reader.GetValue(1));
                            p.NCK = reader.GetString(0);
                            p.MTX = reader.GetInt32(2);
                            p.Nominal_Cycle_Period = reader.GetDouble(3);
                            Table.Add(p);
                        }
                    }
                } //получение информации для таблицы, в которую пользователь заносит номинальное время                 

            }



        }
        public static void GetConfsWithOrdinal()
        {
            //TableWithOrdinals.Clear();
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
                            q.WGHT = Convert.ToDouble(reader.GetValue(1));
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
        }
        /*public static void ReturnGrid() //Заполняет в БД столбец номинальных времен
        {
            string connectionString = "Server=192.168.0.12;Database=PetPro;Password=DbSyS@dm1n;User ID=sa";
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = "192.168.0.12";
            csb.InitialCatalog = "planning";
            csb.UserID = "sa";
            csb.Password = "DbSyS@dm1n";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach(var t in DB.Table) if(t.Modified_Status == true)
                {
                    using(SqlCommand command17 = new SqlCommand("Update_Configuration_Nominal"))
                    {
                        command17.Connection = connection;
                        command17.CommandType = System.Data.CommandType.StoredProcedure;
                        command17.Parameters.Add("@NECK", System.Data.SqlDbType.NVarChar).Value = t.NCK;
                        command17.Parameters.Add("@WEIGHT", System.Data.SqlDbType.Float).Value = t.WGHT;
                        command17.Parameters.Add("@MATRIX", System.Data.SqlDbType.Int).Value = t.MTX;
                        command17.Parameters.Add("@NOMINAL", System.Data.SqlDbType.Float).Value = t.Nominal_Cycle_Period;
                        command17.ExecuteNonQuery();
                    }
                        t.Modified_Status = false;
                }
                
               
            }
        }*/


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
        public static double LoadNominal(int MachineN)
        {


            var q = TableWithOrdinals.Find(p => p.ordinal == MachineN);
            if (q == null)
            {

                return 0;
            }
            var s = Table.Find(v => v.MTX == q.MTX && v.NCK == q.NCK && v.WGHT == q.WGHT);

            return s.Nominal_Cycle_Period;
        }
        public static DateTime LoadStartDate(int MachineN)
        {
            var q = TableWithOrdinals.Find(p => p.ordinal == MachineN);
            if (q == null)
            {
                return DateTime.MinValue;
            }

            return q.startDate;
        }
        public static DateTime LoadEndDate(int MachineN)
        {
            var q = TableWithOrdinals.Find(p => p.ordinal == MachineN);
            if (q == null)
            {
                return DateTime.MinValue;
            }
            return q.endDate;
        }
    }
    public class Configuration
    {
        public double WGHT { get; set; }
        public string NCK { get; set; }
        public int MTX { get; set; }
        public double Nominal_Cycle_Period { get; set; }




    }
    public class ConfigurationWithMachineN
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public double WGHT { get; set; }
        public string NCK { get; set; }
        public int MTX { get; set; }
        public int ordinal { get; set; }
    }
}
