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
    internal static class EmployeeFromDb
    {
        public static async Task<List<Employee>> GetEmployee()
        {
            List<Employee> employee = new List<Employee>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getEmployee = "SELECT * FROM employee_view; ";

                    NpgsqlCommand command = new NpgsqlCommand(getEmployee, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            DateTime birthday = DateTime.Now;
                            if (!(reader[4] is DBNull))
                            {
                                birthday = reader.GetDateTime(4);
                            }
                            employee.Add(new Employee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), birthday));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return employee;
        }

        public static async Task DeleteEmployee(Employee employee)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string delete = "CALL delete_employee(@id_employee)";
                    NpgsqlCommand command = new NpgsqlCommand(delete, connection);
                    command.Parameters.AddWithValue("id_employee", employee.IdEmployee);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"{employee.Fio} удалён");
                    }
                    else
                    {
                        MessageBox.Show($"{employee.Fio} удалён");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task AddEmployee(string fio, string postName, string money, DateTime date)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string add = "INSERT INTO public.employee(fio, id_post, salary, date_of_employment) VALUES (@fio, (SELECT id_post FROM public.post WHERE name_post = @name_post), @salary, @date_of_employment); ";

                    NpgsqlCommand command = new NpgsqlCommand(add, connection);
                    command.Parameters.AddWithValue("fio", fio);
                    command.Parameters.AddWithValue("name_post", postName);
                    command.Parameters.AddWithValue("salary", money);
                    command.Parameters.AddWithValue("date_of_employment", date);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Сотрудник {fio} добавлен");
                    }
                    else
                    {
                        MessageBox.Show($"Сотрудник {fio} добавлен");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task UpdateEmployee(Employee employee)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string update = "UPDATE public.employee SET id_post = (SELECT id_post FROM public.post WHERE name_post = @name_post), salary = @salary, date_of_employment = @date_of_employment WHERE fio = @fio ;";
                    NpgsqlCommand command = new NpgsqlCommand(update, connection);
                    command.Parameters.AddWithValue("id_post", employee.IdEmployee);
                    command.Parameters.AddWithValue("fio", employee.Fio);
                    command.Parameters.AddWithValue("name_post", employee.PostName);
                    command.Parameters.AddWithValue("salary", employee.Salary);
                    command.Parameters.AddWithValue("date_of_employment", employee.DateOfEmployment);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Сотрудник {employee.Fio} обновлен");
                    }
                    else
                    {
                        MessageBox.Show($"Сотрудник {employee.Fio} обновлен");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task<List<Employee>> FilterEmployeeByPost(int idPost)
        {
            List<Employee> filtered_employee = new List<Employee>();
            NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString);

            try
            {
                await connection.OpenAsync();

                string filterEmployee = "SELECT employee.id_employee, employee.fio, post.name_post, employee.salary, employee.date_of_employment FROM employee JOIN post ON employee.id_post = post.id_post WHERE employee.id_post = @id_post; ";

                NpgsqlCommand cmd = new NpgsqlCommand(filterEmployee, connection);
                cmd.Parameters.AddWithValue("id_post", idPost);

                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        filtered_employee.Add(new Employee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4)));
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

            return filtered_employee;
        }
    }
}