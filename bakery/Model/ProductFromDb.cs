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
    internal static class ProductFromDb
    {
        public static async Task<List<Product>> GetProduct()
        {
            List<Product> product = new List<Product>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getProduct = "SELECT * FROM product_info; ";

                    NpgsqlCommand command = new NpgsqlCommand(getProduct, connection);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            DateTime releasses = DateTime.Now;
                            if (!(reader[3] is DBNull))
                            {
                                releasses = reader.GetDateTime(3);
                            }
                            DateTime dateOfManufacture = DateTime.Now;
                            if (!(reader[5] is DBNull))
                            {
                                dateOfManufacture = reader.GetDateTime(5);
                            }
                            product.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), releasses, reader.GetDecimal(4), dateOfManufacture, reader.GetInt32(6)));
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return product;
        }

        public static async Task DeleteProduct(Product product)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string delete = "CALL delete_product(@id_product)";
                    NpgsqlCommand command = new NpgsqlCommand(delete, connection);
                    command.Parameters.AddWithValue("id_product", product.IdProduct);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"{product.NameProduct} удалён");
                    }
                    else
                    {
                        MessageBox.Show($"{product.NameProduct} удалён");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static async Task AddProduct(string product, string employee, DateTime daterealeases, int price, DateTime datemanf, int quantity)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string add = "INSERT INTO public.product(product_name, id_employee, releases, price, date_of_manufacture, quantity) VALUES (@product_name, (SELECT id_employee FROM public.employee WHERE fio = @fio), @releases, @price, @date_of_manufacture, @quantity); ";
                    
                    NpgsqlCommand command = new NpgsqlCommand(add, connection);
                    command.Parameters.AddWithValue("product_name", product);
                    command.Parameters.AddWithValue("fio", employee);
                    command.Parameters.AddWithValue("releases", daterealeases);
                    command.Parameters.AddWithValue("price", price);
                    command.Parameters.AddWithValue("date_of_manufacture", datemanf);
                    command.Parameters.AddWithValue("quantity", quantity);

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show($"Продукт {product} quantity");
                    }
                    else
                    {
                        MessageBox.Show($"Продукт {product} добавлен");
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
