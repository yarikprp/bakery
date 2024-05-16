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
    internal static class ReceiptWarehouseFromDb
    {
        public static async Task<List<ReceiptWarehouse>> GetReceiptWarehouse()
        {
            List<ReceiptWarehouse> receiptWarehouses = new List<ReceiptWarehouse>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getReceiptWarehouse = "SELECT * FROM receipt_details_view; ";

                    NpgsqlCommand command = new NpgsqlCommand(getReceiptWarehouse, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            DateTime datereceipt = DateTime.Now;
                            if (!(reader[4] is DBNull))
                            {
                                datereceipt = reader.GetDateTime(4);
                            }
                            receiptWarehouses.Add(new ReceiptWarehouse(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), datereceipt, reader.GetString(5)));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return receiptWarehouses;
        }

        public static async Task DeleteReceiptWarehouse(ReceiptWarehouse receiptWarehouse)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string delete = "CALL delete_receipt_warehouse(@id_receipt_warehouse)";
                    NpgsqlCommand command = new NpgsqlCommand(delete, connection);
                    command.Parameters.AddWithValue("id_receipt_warehouse", receiptWarehouse.IdBalance);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Товар удалён");
                    }
                    else
                    {
                        MessageBox.Show($"Товар удалён");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task AddReceiptWarehouse(string ingredient, string product, int company, DateTime date, int quantitys)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string add = "INSERT INTO public.receipt_warehouse(id_ingredients, id_product, id_supplier, date_of_receipt, quantity) VALUES ((SELECT id_ingredients FROM public.ingredients WHERE name_ingredients = @name_ingredients), (SELECT id_product FROM public.product WHERE product_name = @product_name), (SELECT id_supplier FROM public.supplier WHERE id_supplier = @id_supplier), @date_of_receipt, @quantity); ";

                    NpgsqlCommand command = new NpgsqlCommand(add, connection);
                    command.Parameters.AddWithValue("name_ingredients", ingredient);
                    command.Parameters.AddWithValue("product_name", product);
                    command.Parameters.AddWithValue("id_supplier", company);
                    command.Parameters.AddWithValue("date_of_receipt", date);
                    command.Parameters.AddWithValue("quantity", quantitys);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Остаток добавлен");
                    }
                    else
                    {
                        MessageBox.Show($"Остаток добавлен");
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
