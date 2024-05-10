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
    internal static class UnitFromDb
    {
        public static async Task<List<Unit>> GetUnit()
        {
            List<Unit> unit = new List<Unit>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getUnit = "SELECT * FROM public.unit; ";

                    NpgsqlCommand command = new NpgsqlCommand(getUnit, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            unit.Add(new Unit(reader.GetInt32(0), reader.GetString(1)));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return unit;
        }
    }
}
