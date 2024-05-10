using bakery.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bakery.Model
{
    internal static class SupplierFromDb
    {
        public static async Task<List<Supplier>> GetSupplier()
        {
            List<Supplier> supplier = new List<Supplier>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getSupplier = "SELECT * FROM public.supplier; ";

                    NpgsqlCommand command = new NpgsqlCommand(getSupplier, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            supplier.Add(new Supplier(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return supplier;
        }
    }
}