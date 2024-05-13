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
                            employee.Add(new Employee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDecimal(3), birthday));
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
    }
}