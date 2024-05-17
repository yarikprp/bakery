using bakery.Classes;
using kulinaria_app_v2.Classes;
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
                        MessageBox.Show($"Поставщик {ingredient} добавлен");
                    }
                    else
                    {
                        MessageBox.Show($"Поставщик {ingredient} добавлен");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       public static async Task UpdateCompany(Supplier supplier)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string update = "UPDATE public.supplier SET id_company = (SELECT id_company FROM public.company WHERE company_name = @company_name) WHERE ingredient = @ingredient AND id_supplier = @id_supplier; ";
                    NpgsqlCommand command = new NpgsqlCommand(update, connection);
                    command.Parameters.AddWithValue("id_supplier", supplier.IdSupplier);
                    command.Parameters.AddWithValue("ingredient", supplier.Ingredient);
                    command.Parameters.AddWithValue("company_name", supplier.NameCompany);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Поставщик {supplier.Ingredient} добавлен");
                    }
                    else
                    {
                        MessageBox.Show($"Поставщик {supplier.Ingredient} добавлен");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task<List<Supplier>> FilterUserByCompany(int idCompany)
        {
            List<Supplier> filtered_supplier = new List<Supplier>();
            NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString);

            try
            {
                await connection.OpenAsync();

                string filterSupplier = "SELECT supplier.id_supplier, supplier.ingredient, company.company_name FROM supplier JOIN company ON supplier.id_company = company.id_company WHERE company.id_company = @id_company;";

                NpgsqlCommand cmd = new NpgsqlCommand(filterSupplier, connection);
                cmd.Parameters.AddWithValue("@id_company", idCompany);

                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        filtered_supplier.Add(new Supplier(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
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

            return filtered_supplier;
        }

    }
}