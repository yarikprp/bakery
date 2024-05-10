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
    internal static class PostFromDb
    {
        public static async Task<List<Post>> GetPost()
        {
            List<Post> post = new List<Post>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getPost = "SELECT * FROM public.post ;";

                    NpgsqlCommand command = new NpgsqlCommand(getPost, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            post.Add(new Post(reader.GetInt32(0), reader.GetString(1)));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return post;
        }
    }
}
