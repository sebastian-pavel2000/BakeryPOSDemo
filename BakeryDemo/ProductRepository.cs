using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BakeryDemo
{
    internal sealed class ProductRepository
    {
        public DataTable GetAll(string search = null)
        {
            using (var conn = Db.Get())
            {
                using (var cmd = conn.CreateCommand())
                {
                    if (string.IsNullOrWhiteSpace(search))
                    {
                        cmd.CommandText = "SELECT ProduktID, Name, Preis FROM Produkte ORDER BY Name";
                    }
                    else
                    {
                        cmd.CommandText = "SELECT ProduktID, Name, Preis FROM Produkte WHERE Name LIKE @s ORDER BY Name";
                        cmd.Parameters.AddWithValue("@s", "%" + search + "%");
                    }

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}
