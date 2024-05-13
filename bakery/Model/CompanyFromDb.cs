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
    internal static class CompanyFromDb
    {
        public static async Task<List<Company>> GetCompany()
        {
            List<Company> companies = new List<Company>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getCompany = "SELECT * FROM public.company; ";

                    NpgsqlCommand command = new NpgsqlCommand(getCompany, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            companies.Add(new Company(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return companies;
        }

        public static async Task DeleteCompany(Company company)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string delete = "CALL delete_company(@id_company)";
                    NpgsqlCommand command = new NpgsqlCommand(delete, connection);
                    command.Parameters.AddWithValue("id_company", company.IdCompany);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Компания удалёна");
                    }
                    else
                    {
                        MessageBox.Show($"Компания удалёна");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        public static async Task AddCompany(string name, int prot, int fats, int carb)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string add = "CALL public.insert_company(prod_name, prod_protein, prod_fats, prod_carboh) " +
                        "VALUES (@name, @prot, @fats, @carb)";
                    NpgsqlCommand command = new NpgsqlCommand(add, connection);
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("prot", prot);
                    command.Parameters.AddWithValue("fats", fats);
                    command.Parameters.AddWithValue("carb", carb);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Продукт {name} добавлен");
                    }
                    else
                    {
                        MessageBox.Show("Запрос отклонён");
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
