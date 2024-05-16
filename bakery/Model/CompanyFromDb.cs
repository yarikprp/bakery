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

        public static async Task AddCompany(string nameCompany, string fio, string namePhone, string adress)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string add = "CALL public.insert_company(@company_name, @fio, @number_phone, @adress); ";

                    NpgsqlCommand command = new NpgsqlCommand(add, connection);
                    command.Parameters.AddWithValue("company_name", nameCompany);
                    command.Parameters.AddWithValue("fio", fio);
                    command.Parameters.AddWithValue("number_phone", namePhone);
                    command.Parameters.AddWithValue("adress", adress);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Компания {nameCompany} добавлена");
                    }
                    else
                    {
                        MessageBox.Show($"Компания {nameCompany} добавлена");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task UpdateCompany(Company company)
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
                        MessageBox.Show($"Компания {company.NameCompany} обновлена");
                    }
                    else
                    {
                        MessageBox.Show($"Компания {company.NameCompany} обновлена");
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
