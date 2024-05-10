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

                    string getReceiptWarehouse = "SELECT * FROM public.receipt_warehouse; ";

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
                            receiptWarehouses.Add(new ReceiptWarehouse(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), datereceipt, reader.GetString(5)));
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
    }
}
