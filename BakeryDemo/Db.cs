using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BakeryDemo
{
    internal static class Db
    {
        // Adjust only if your SQL instance is different
        public static readonly string Conn =
            "Server=(localdb)\\MSSQLLocalDB;Database=BackNetDemo;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;";

        public static SqlConnection Get() => new SqlConnection(Conn);
    }
}
