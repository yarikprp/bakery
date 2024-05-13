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

                    string getSupplier = "SELECT * FROM supplier_company_view; ";

                    NpgsqlCommand command = new NpgsqlCommand(getSupplier, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            supplier.Add(new Supplier(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
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

        public static async Task DeleteSupplier(Supplier supplier)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string delete = "CALL delete_supplier(@id_supplier)";
                    NpgsqlCommand command = new NpgsqlCommand(delete, connection);
                    command.Parameters.AddWithValue("id_supplier", supplier.IdSupplier);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Поставщик удалён");
                    }
                    else
                    {
                        MessageBox.Show($"Поставщик удалён");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task AddSupplier(string ingredient, string nameCompany)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string add = "CALL insert_supplier(@ingredient, @company_name); ";

                    NpgsqlCommand command = new NpgsqlCommand(add, connection);
                    command.Parameters.AddWithValue("ingredient", ingredient);
                    command.Parameters.AddWithValue("company_name", nameCompany);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Компания {ingredient} добавлена");
                    }
                    else
                    {
                        MessageBox.Show($"Компания {ingredient} добавлена");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       /* public static async Task UpdateCompany(Supplier supplier)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string update = "CALL update_company(@id, @company_name, @fio, @number_phone, @adress);";
                    NpgsqlCommand command = new NpgsqlCommand(update, connection);
                    command.Parameters.AddWithValue("id", company.IdCompany);
                    command.Parameters.AddWithValue("company_name", company.NameCompany);
                    command.Parameters.AddWithValue("fio", company.Fio);
                    command.Parameters.AddWithValue("number_phone", company.NamePhone);
                    command.Parameters.AddWithValue("adress", company.Adress);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show("Компания обновлена");
                    }
                    else
                    {
                        MessageBox.Show("Компания обновлена");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/
    }
}