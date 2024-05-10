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
    internal static class TypeIngredientsFromDb
    {
        public static async Task<List<TypeIngredients>> GetTypeIngredients()
        {
            List<TypeIngredients> typeIngredients = new List<TypeIngredients>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getTypeIngredients = "SELECT * FROM public.type_ingredients; ";

                    NpgsqlCommand command = new NpgsqlCommand(getTypeIngredients, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            typeIngredients.Add(new TypeIngredients(reader.GetInt32(0), reader.GetString(1)));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return typeIngredients;
        }
    }
}
