using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BakeryDemo
{
    internal sealed class OrderRepository
    {
        public int SaveOrder(IEnumerable<CartItem> items, decimal total)
        {
            using (var conn = Db.Get())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert order
                        using (var cmdOrder = new SqlCommand(
                            "INSERT INTO Bestellungen (Gesamtpreis) OUTPUT INSERTED.BestellungID VALUES (@total);",
                            conn, tx))
                        {
                            cmdOrder.Parameters.AddWithValue("@total", total);
                            var orderId = (int)cmdOrder.ExecuteScalar();

                            // Insert lines
                            foreach (var it in items)
                            {
                                using (var cmdLine = new SqlCommand(@"
                                    INSERT INTO Bestellpositionen (BestellungID, ProduktID, Menge, Preis)
                                    VALUES (@bid, @pid, @menge, @preis);", conn, tx))
                                {
                                    cmdLine.Parameters.AddWithValue("@bid", orderId);
                                    cmdLine.Parameters.AddWithValue("@pid", it.ProduktID);
                                    cmdLine.Parameters.AddWithValue("@menge", it.Menge);
                                    cmdLine.Parameters.AddWithValue("@preis", it.Preis);
                                    cmdLine.ExecuteNonQuery();
                                }
                            }

                            tx.Commit();
                            return orderId;
                        }
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
