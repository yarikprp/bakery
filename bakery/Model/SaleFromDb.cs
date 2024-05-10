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
    internal static class SaleFromDb
    {
        public static async Task<List<Sale>> GetSale()
        {
            List<Sale> sale = new List<Sale>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getSale = "SELECT * FROM public.sale; ";

                    NpgsqlCommand command = new NpgsqlCommand(getSale, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            DateTime date_of_sale = DateTime.Now;
                            if (!(reader[3] is DBNull))
                            {
                                date_of_sale = reader.GetDateTime(3);
                            }
                            sale.Add(new Sale(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), date_of_sale, reader.GetInt32(4)));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return sale;
        }
    }
}
