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
            var dat = new DateTime(2022, 07, 20);
           for(int i = 0; i < 120; i++)
            {
                Console.WriteLine($"{dat.AddHours(i)}");
            }

            Console.WriteLine(dat);
            Console.ReadLine();
        }
    }
    
    
    
}
