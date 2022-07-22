using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Time
{
    class Time_Intervals_Preparation
    {

        public static List<(DateTime,double)> GetCycleIntervals_Start(int N)
        {
            List<(DateTime, double)> L = new List<(DateTime, double)>();
            string connectionString = "Server=192.168.0.12;Database=Planning;Password=DbSyS@dm1n;User ID=sa";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    //command.CommandText = $"DECLARE @dnt DateTime SET @dnt = (SELECT[date] FROM[PetPro].[dbo].[Time_Of_Changing_Orders] WHERE MachineN = {N}) SELECT[Date],Cycle_Time FROM[PetPro].[dbo].[Cycle_Photometer] WHERE[Date] > @dnt AND MachineNumber = {N} ORDER BY[Date]";
                    command.CommandText = @"select Cycle_Photometer.Date,Cycle_Photometer.Cycle_time from Cycle_Photometer
inner join Time_Of_Changing_Orders on Time_Of_Changing_Orders.MachineN = Cycle_Photometer.MachineNumber
where Cycle_Photometer.date >= Time_Of_Changing_Orders.[date] and Cycle_Photometer.MachineNumber = @eq_id";
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@eq_id", N);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            L.Add((reader.GetDateTime(0), reader.GetInt32(1)));
                        }
                    }
                }
            }
            /*
             * 
             * 
             * 
             * 
             */
            return L;
        }





        


    }
}
