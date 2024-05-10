﻿using bakery.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bakery.Model
{
    internal static class ConsumptionFromDb
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

    }
}
