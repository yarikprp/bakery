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

                    string getProductReleasePlan = "SELECT * FROM product_release_plan_view; ";

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
                            productReleasePlans.Add(new ProductReleasePlan(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), release));
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

        public static async Task DeleteProductReleasePlan(ProductReleasePlan productReleasePlan)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string delete = "CALL delete_product_release_plan(@id_product_release_plan)";
                    NpgsqlCommand command = new NpgsqlCommand(delete, connection);
                    command.Parameters.AddWithValue("id_product_release_plan", productReleasePlan.IdPlan);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"План удалён");
                    }
                    else
                    {
                        MessageBox.Show($"План удалён");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task AddProductReleasePlan(string product, string fio, DateTime date)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string add = "INSERT INTO public.product_release_plan(id_product, id_employee, planned_release_date) VALUES ((SELECT id_employee FROM public.employee WHERE fio = @fio), (SELECT id_product FROM public.product WHERE product_name = @product_name), @planned_release_date); ";
                    

                    NpgsqlCommand command = new NpgsqlCommand(add, connection);
                    command.Parameters.AddWithValue("product_name", product);
                    command.Parameters.AddWithValue("fio", fio);
                    command.Parameters.AddWithValue("planned_release_date", date);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"План добавлен");
                    }
                    else
                    {
                        MessageBox.Show($"План добавлен");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
