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
    internal class ProductReleasePlanFromDb
    {
        public static async Task<List<ProductReleasePlan>> GetProductReleasePlan()
        {
            List<ProductReleasePlan> productReleasePlans = new List<ProductReleasePlan>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getProductReleasePlan = "SELECT * FROM public.product_release_plan; ";

                    NpgsqlCommand command = new NpgsqlCommand(getProductReleasePlan, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            DateTime release = DateTime.Now;
                            if (!(reader[3] is DBNull))
                            {
                                release = reader.GetDateTime(3);
                            }
                            productReleasePlans.Add(new ProductReleasePlan(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), release));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return productReleasePlans;
        }
    }
}
