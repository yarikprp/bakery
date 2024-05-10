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
    public static class ConsumptionFromDb
    {
        public static async Task<List<ConsumptionOfIngredients>> GetConsumptionOfIngredients()
        {
            List<ConsumptionOfIngredients> consumptionOfIngredients = new List<ConsumptionOfIngredients>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getConsumptionOfIngredients = "SELECT * FROM public.consumption_of_ingredients; ";

                    NpgsqlCommand command = new NpgsqlCommand(getConsumptionOfIngredients, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            consumptionOfIngredients.Add(new ConsumptionOfIngredients(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return consumptionOfIngredients;
        }

        public static async Task DeleteConsumptionOfIngredients(ConsumptionOfIngredients consumptionOfIngredients)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string delete = "CALL delete_consumption_of_ingredients(@consumption_id)";
                    NpgsqlCommand command = new NpgsqlCommand(delete, connection);
                    command.Parameters.AddWithValue("consumption_id", consumptionOfIngredients.IdConsumption);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Расход удалён");
                    }
                    else
                    {
                        MessageBox.Show($"Расход удалён");
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
