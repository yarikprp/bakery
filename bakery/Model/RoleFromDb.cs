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
    internal static class RoleFromDb
    {
        public static async Task<List<Role>> GetRoles()
        {
            List<Role> roles = new List<Role>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getRoles = "SELECT * FROM public.role_view ;";

                    NpgsqlCommand command = new NpgsqlCommand(getRoles, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            roles.Add(new Role(reader.GetInt32(0), reader.GetString(1)));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return roles;
        }
    }
}
