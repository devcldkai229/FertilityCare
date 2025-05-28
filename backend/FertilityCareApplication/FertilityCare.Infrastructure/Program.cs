using FertilityCare.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connectionString = "Server=localhost,1433;Database=FeritilyCareDB;UID=sa;PWD=12345;TrustServerCertificate=True;Encrypt=False";

            using (var context = new FertilityCareDBContext())
            {
                bool canConnect = context.Database.CanConnect();
                Console.WriteLine(canConnect ? "Kết nối thành công" : "Không thể kết nối đến database");
                if(context.TreatmentServices.Count() > 0)
                {
                    Console.WriteLine("yes");
                }
                else
                {
                    Console.WriteLine("no");
                }
            }

        }
    }
}
