using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


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
       

        public static List<(DateTime,double)> GetCycleIntervals_Start(int N)
        {
            List<(DateTime, double)> L = new List<(DateTime, double)>();
            string connectionString = "Server=192.168.0.12;Database=PetPro;Password=DbSyS@dm1n;User ID=sa";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    //command.CommandText = $"DECLARE @dnt DateTime SET @dnt = (SELECT[date] FROM[PetPro].[dbo].[Time_Of_Changing_Orders] WHERE MachineN = {N}) SELECT[Date],Cycle_Time FROM[PetPro].[dbo].[Cycle_Photometer] WHERE[Date] > @dnt AND MachineNumber = {N} ORDER BY[Date]";
                    command.CommandText = @"select Cycle_Photometer.Date,Cycle_Photometer.Cycle_time from Cycle_Photometer
inner join Time_Of_Changing_Orders on Time_Of_Changing_Orders.MachineN = Cycle_Photometer.MachineNumber
where Cycle_Photometer.date >= Time_Of_Changing_Orders.[date] and Cycle_Photometer.MachineNumber = @eq_id ORDER BY [Date] ASC";
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@eq_id", N);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            L.Add((reader.GetDateTime(0), (reader.IsDBNull(1)?Double.NaN:reader.GetDouble(1))));
                        }
                    }
                }
            }
            
            /*var tmp = from l in L
                      select l.Item2;
            return tmp.ToList();*/
            //return L.Select(l=>l.Item2).ToList();
            var O = new List<(DateTime, double)>();
            for (int j= 1; j < L.Count-1; j++)
            {
                
                if ((L[j].Item1 - L[j - 1].Item1).TotalHours > 1)
                {
                    //int index = L.IndexOf((L[j-1].Item1,L[j-1].Item2));
                    O.Add((L[j - 1].Item1, L[j - 1].Item2));
                    


                }
            }
            for(int k = 0; k < O.Count; k++)
            {
                int index = L.IndexOf((O[k].Item1, O[k].Item2));
                L.Insert(index+1, (O[k].Item1.AddHours(1), Double.NaN));

            }

            return L;
        }
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
                DateTime d = new DateTime(2001, 11, 09);
                return d;
            }

            return q.startDate;
        }
        public static DateTime LoadEndDate(int MachineN)
        {
            var q = TableWithOrdinals.Find(p => p.ordinal == MachineN);
            if (q == null)
            {
                DateTime d = new DateTime(2001, 11, 09);
                return d;
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
