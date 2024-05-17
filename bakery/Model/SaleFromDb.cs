using bakery.Classes;
using kulinaria_app_v2.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

                    string getSale = "SELECT * FROM sale_view; ";

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
                            sale.Add(new Sale(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), date_of_sale, reader.GetInt32(4)));
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

        public static async Task DeleteSale(Sale sale)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string delete = "CALL delete_sale(@id_sale)";
                    NpgsqlCommand command = new NpgsqlCommand(delete, connection);
                    command.Parameters.AddWithValue("id_sale", sale.IdSale);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Продажа удалёна");
                    }
                    else
                    {
                        MessageBox.Show($"Продажа удалёна");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static async Task AddSale(int plan, string employee, DateTime dateSale, int quantitys)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string add = "INSERT INTO public.sale(id_plan, id_employee, date_of_sale, quantity) VALUES ((SELECT id_plan FROM public.product_release_plan WHERE id_plan = @id_plan), (SELECT id_employee FROM public.employee WHERE fio = @fio), @date_of_sale, @quantity); ";
                    

                    NpgsqlCommand command = new NpgsqlCommand(add, connection);
                    command.Parameters.AddWithValue("id_plan", plan);
                    command.Parameters.AddWithValue("fio", employee);
                    command.Parameters.AddWithValue("date_of_sale", dateSale);
                    command.Parameters.AddWithValue("quantity", quantitys);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Продажа {dateSale} добавлена");
                    }
                    else
                    {
                        MessageBox.Show($"Продажа {dateSale} добавлена");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task UpdateSale(Sale sale)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string update = "UPDATE public.sale SET id_plan = (SELECT id_plan FROM public.product_release_plan WHERE id_plan = @id_plan), id_employee = (SELECT id_employee FROM public.employee WHERE fio = @fio), date_of_sale = @date_of_sale, quantity = @quantity WHERE id_sale = @id_sale; ";
                    NpgsqlCommand command = new NpgsqlCommand(update, connection);
                    command.Parameters.AddWithValue("id_sale", sale.IdSale);
                    command.Parameters.AddWithValue("id_plan", sale.IdPlan);
                    command.Parameters.AddWithValue("fio", sale.Fio);
                    command.Parameters.AddWithValue("date_of_sale", sale.DateOfSale);
                    command.Parameters.AddWithValue("quantity", sale.Quantity);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Продажа {sale.DateOfSale} обновлена");
                    }
                    else
                    {
                        MessageBox.Show($"Продажа {sale.DateOfSale} обновлена");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static async Task<List<Sale>> FilterSaleByPlan(int idPlan)
        {
            List<Sale> filtered_sale = new List<Sale>();
            NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString);

            try
            {
                await connection.OpenAsync();

                string filterSale = "SELECT s.id_sale, prp.id_plan, e.fio, s.date_of_sale, s.quantity FROM sale s JOIN product_release_plan prp ON s.id_plan = prp.id_plan JOIN employee e ON s.id_employee = e.id_employee WHERE prp.id_plan = @id_plan; ";

                NpgsqlCommand cmd = new NpgsqlCommand(filterSale, connection);
                cmd.Parameters.AddWithValue("id_plan", idPlan);

                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        DateTime date_of_sale = DateTime.Now;
                        if (!(reader[3] is DBNull))
                        {
                            date_of_sale = reader.GetDateTime(3);
                        }
                        filtered_sale.Add(new Sale(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), date_of_sale, reader.GetInt32(4)));
                    }
                    await reader.CloseAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }

            return filtered_sale;
        }
    }
}
