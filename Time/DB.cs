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
        public static List<Configuration> Table = new List<Configuration>();
        public static List<ConfigurationWithMachineN> TableWithOrdinals = new List<ConfigurationWithMachineN>();
        static int N = 120;

        public static void GetListOfConfigurations()
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
                using (SqlCommand command1 = new SqlCommand())
                {
                    command1.CommandText = "SELECT * FROM Sets";
                    command1.Connection = connection;
                    SqlDataReader reader = command1.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read()) // построчно считываем данные
                        {
                            Configuration p = new Configuration();                            
                            p.WGTH = Convert.ToDouble(reader.GetValue(2));                            
                            p.NCK = reader.GetString(1);
                            p.MTX = Convert.ToInt32(reader.GetString(3));     
                            Table.Add(p);
                        }
                    }
                } //получение информации для таблицы, в которую пользователь заносит номинальное время                 

            }

            

        }
        public static void GetConfsWithOrdinal()
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
                            q.ordinal = (reader.GetString(5).Substring(7).StartsWith("0"))? Convert.ToInt32(reader.GetString(5).Substring(8)) : Convert.ToInt32(reader.GetString(5).Substring(7));
                            TableWithOrdinals.Add(q);
                        }
                    }
                }
            }
        }
        /*public static void ReturnGrid() //Заполняет в БД столбец номинальных времен
        {
            string connectionString = "Server=192.168.0.12;Database=Planning;Password=DbSyS@dm1n;User ID=sa";
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

                }
            }
        }*/
        /*using (SqlCommand command2 = new SqlCommand())
                {
                    command2.CommandText = "CREATE #WriteNominal (Weigth NVARCHAR(max), Neck NVARCHAR(max), Matrix NVARCHAR(max), Nominal_Period DECIMAL(9))" +
                        "";
                    command2.Connection = connection;                


                } //2-4 занесение информации, полученной выше, и номиналльных периодов в другую таблицу
                using (SqlCommand command3 = new SqlCommand())
                {
                    string insertStmt = "INSERT INTO #WriteNominal(Weight, Neck, Matrix, Nominal_Period) " +
                    "VALUES(@WGHT, @NCK, @MTX, @NomPer)";
                    command3.CommandText = insertStmt;
                    command3.Connection = connection;

                    command3.Parameters.Add("@WGHT");
                    command3.Parameters.Add("@NCK");
                    command3.Parameters.Add("@MTX");
                    command3.Parameters.Add("@NomPer");

                    foreach (WeightNeckMtxPeriod item in Table)
                    {
                        command3.Parameters["@WGHT"].Value = item.WGTH;
                        command3.Parameters["@NCK"].Value = item.NCK;
                        command3.Parameters["@MTX"].Value = item.MTX;
                        command3.Parameters["@NomPer"].Value = item.Nominal_Cycle_Period;
                        command3.ExecuteNonQuery();
                    }
                }
                using (SqlCommand command4 = new SqlCommand())
                {
                    command4.Connection = connection;
                    command4.CommandText = "MERGE Target_Table_Name AS TARGET USING #WriteNominal AS SOURCE" +
                        "ON (TARGET.Weight = SOURCE.Weight" +
                        "AND TARGETT.Neck = SOURCE.Neck" +
                        "AND TARGET.Matrix = SOURCE.Matrix) " +
                        "WHEN MATCHED THEN" +
                        " UPDATE SET" +
                        " TARGET.Nominal_Period = SOURCE.Nominal_Period" +
                        " WHEN NOT MATCHED THEN " +
                        "INSERT (Weight, Neck, Matrix, Nominal_Period) VALUES(SOURCE.Weight,SOURCE.Neck,SOURCE.Matrix,SOURCE.Nominal_Period) " +
                        " WHEN NOT MATCHED BY SOURCE THEN DELETE";
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
        public static double RandomNominal(int MachineN)
        {

            
            var q = TableWithOrdinals.Find(p => p.ordinal == MachineN);
            var s = Table.Find(v=>v.MTX==q.MTX && v.NCK==q.NCK && v.WGTH==q.WGTH);
            
            return s.Nominal_Cycle_Period;
        }
        
    }
    public class Configuration
    {       
        
        public double WGTH { get; set; }
        public string NCK { get; set; }
        public int MTX { get; set; }
        public double Nominal_Cycle_Period { get; set; }

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
