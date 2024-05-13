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
    internal static class IngredientsFromDb
    {
        public static async Task<List<Ingredients>> GetIngredients()
        {
            List<Ingredients> ingredients = new List<Ingredients>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getIngredients = "SELECT * FROM ingredients_view; ";

                    NpgsqlCommand command = new NpgsqlCommand(getIngredients, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            ingredients.Add(new Ingredients(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6)));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return ingredients;
        }

        public static async Task DeleteIngredients(Ingredients ingredients)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string delete = "CALL delete_ingredient(@id_ingredients)";
                    NpgsqlCommand command = new NpgsqlCommand(delete, connection);
                    command.Parameters.AddWithValue("id_ingredients", ingredients.IdIngredients);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"{ingredients.NameIngredients} удалён");
                    }
                    else
                    {
                        MessageBox.Show($"{ingredients.NameIngredients} удалён");
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
