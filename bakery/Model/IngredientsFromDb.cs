﻿using bakery.Classes;
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
        public static async Task AddIngredients(string ingredient, string type, string product, string unit, int quantity, string warehouse)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string add = "CALL insert_ingredient(@type_ingredient, @product_name, @name_unit, @quantity, @warehouse, @name_ingredients)";
                    
                    NpgsqlCommand command = new NpgsqlCommand(add, connection);
                    command.Parameters.AddWithValue("type_ingredient", type);
                    command.Parameters.AddWithValue("product_name", product);
                    command.Parameters.AddWithValue("name_unit", unit);
                    command.Parameters.AddWithValue("quantity", quantity);
                    command.Parameters.AddWithValue("warehouse", warehouse);
                    command.Parameters.AddWithValue("name_ingredients", ingredient);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Ингредиент {ingredient} добавлен");
                    }
                    else
                    {
                        MessageBox.Show($"Ингредиент {ingredient} добавлен");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task UpdateIngredients(Ingredients ingredients)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string update = "UPDATE public.ingredients SET id_type = (SELECT id_type FROM public.type_ingredients WHERE type_ingredient = @type_ingredient), id_product = (SELECT id_product FROM public.product WHERE product_name = @product_name), id_unit = (SELECT id_unit FROM public.unit WHERE name_unit = @name_unit), quantity = @quantity, warehouse = @warehouse, name_ingredients = @name_ingredients WHERE id_ingredients = @id_ingredients; ";
                    NpgsqlCommand command = new NpgsqlCommand(update, connection);
                    command.Parameters.AddWithValue("id_ingredients", ingredients.IdIngredients);
                    command.Parameters.AddWithValue("type_ingredient", ingredients.TypeIngredients);
                    command.Parameters.AddWithValue("product_name", ingredients.NameProduct);
                    command.Parameters.AddWithValue("name_unit", ingredients.NameUnit);
                    command.Parameters.AddWithValue("quantity", ingredients.Quantity);
                    command.Parameters.AddWithValue("warehouse", ingredients.Warehouse);
                    command.Parameters.AddWithValue("name_ingredients", ingredients.NameIngredients);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Ингредиент {ingredients.NameIngredients} обновлен");
                    }
                    else
                    {
                        MessageBox.Show($"Ингредиент {ingredients.NameIngredients} обновлен");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static async Task<List<Ingredients>> FilterUserByRole(int idType)
        {
            List<Ingredients> filtered_type = new List<Ingredients>();
            NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString);

            try
            {
                await connection.OpenAsync();

                string filterIngredients = "SELECT i.id_ingredients, ti.type_ingredient, p.product_name, u.name_unit, i.quantity, i.warehouse, i.name_ingredients FROM ingredients i JOIN type_ingredients ti ON i.id_type = ti.id_type JOIN unit u ON i.id_unit = u.id_unit JOIN product p ON i.id_product = p.id_product WHERE ti.id_type = @id_type; ";

                NpgsqlCommand cmd = new NpgsqlCommand(filterIngredients, connection);
                cmd.Parameters.AddWithValue("id_type", idType);

                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        filtered_type.Add(new Ingredients(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6)));
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

            return filtered_type;
        }

    }
}
