using bakery.Classes;
using kulinaria_app_v2.Classes;
using MimeKit;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bakery.Model
{
    internal static class UserFromDb
    {
        public static async Task<User> GetUser(string login, string password)
        {
            User user = null;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string getUser = "SELECT * FROM public.tb_user where login = @login";
                    NpgsqlCommand command = new NpgsqlCommand(getUser, connection);
                    command.Parameters.AddWithValue("login", login);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();

                        if (password != "")
                        {
                            if (Verification.VerifySHA512Hash(password, (string)reader["user_password"]))
                            {
                                DateTime birthday = DateTime.Now;
                                if (!(reader[4] is DBNull))
                                {
                                    birthday = reader.GetDateTime(4);
                                }
                                user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), birthday,
                                    reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetInt32(9), reader.GetString(10));
                            }
                            else
                            {
                                MessageBox.Show("Неверный пароль");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Нет такого пользователя");
                        }
                    }
                    await reader.CloseAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return user;
        }

        public static async Task UpdateUserProfile(User user)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string update = "UPDATE public.tb_user SET first_name=@fName, last_name=@lName, patronymic=@patronymic, " +
                        "date_of_birthday=@birthday, phone=@phone, adress=@adress " +
                        "WHERE user_id=@id;";
                    NpgsqlCommand command = new NpgsqlCommand(update, connection);
                    command.Parameters.AddWithValue("fName", user.FirstName);
                    command.Parameters.AddWithValue("lName", user.LastName);
                    command.Parameters.AddWithValue("patronymic", user.Patronymic);
                    command.Parameters.AddWithValue("birthday", user.DateOfBirthday);
                    command.Parameters.AddWithValue("phone", user.Phone);
                    command.Parameters.AddWithValue("adress", user.Adress);
                    command.Parameters.AddWithValue("id", user.UserId);

                    await command.ExecuteNonQueryAsync();
                }
            }

            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task ChangePassword(int idUser, string newPass)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string change = "UPDATE public.tb_user SET user_password=@new " +
                        "WHERE user_id = @id";

                    NpgsqlCommand command = new NpgsqlCommand(change, connection);
                    command.Parameters.AddWithValue("id", idUser);
                    command.Parameters.AddWithValue("new", Verification.GetSHA512Hash(newPass));

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        MessageBox.Show("Пароль изменён");
                    }
                    else
                    {
                        MessageBox.Show("Запрос отклонён");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static bool CheckPassword(string password, string passRepeat)
        {
            if (password.Length < 6)
            {
                MessageBox.Show("Длина пароля должна быть больше 6 символов");
                return false;
            }
            else
            {
                bool f, f1, f2;

                f = f1 = f2 = false;

                for (int i = 0; i < password.Length; i++)
                {
                    if (char.IsDigit(password[i]))
                        f1 = true;

                    if (char.IsUpper(password[i]))
                        f2 = true;

                    if (f1 && f2)
                        break;
                }

                if (!(f1 && f2))
                {
                    MessageBox.Show("Пароль должен содержать хотя бы одну цифру или заглавную букву");
                    return f1 && f2;
                }
                else
                {
                    string specSymbols = "!@#$%^";
                    for (int i = 0; i < password.Length; i++)
                    {
                        for (int j = 0; j < specSymbols.Length; j++)
                        {
                            if (password[i] == specSymbols[j])
                            {
                                f = true;
                                break;
                            }
                        }
                    }

                    if (!f)
                        MessageBox.Show("Пароль должен содеожать один из специальных символов");

                    if ((password == passRepeat) && f)
                        return true;
                    else
                    {
                        MessageBox.Show("Пароли не совпадают, подтвердите пароль");
                        return false;
                    }
                }
            }
        }

        public static async Task<bool> CheckUser(string login)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string check = "select login from tb_user where login = @login";
                    NpgsqlCommand command = new NpgsqlCommand(check, connection);
                    command.Parameters.AddWithValue("login", login);

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Такой логин уже есть");
                        return false;
                    }
                    else
                    {
                        await reader.CloseAsync();
                        return true;
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static async Task AddUser(string login, string password, string firstName, string lastName, string email)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(DbConnection.ConnectionString))
                {
                    await connection.OpenAsync();

                    string add = "INSERT INTO public.tb_user (first_name, last_name, patronymic, login, user_password, phone, adress, id_role, email) " +
                        "VALUES (@firstName, @lastName, '', @login, @password, '', '', default, @email)";
                    NpgsqlCommand command = new NpgsqlCommand(add, connection);
                    command.Parameters.AddWithValue("firstName", firstName);
                    command.Parameters.AddWithValue("lastName", lastName);
                    command.Parameters.AddWithValue("login", login);
                    command.Parameters.AddWithValue("email", email);
                    command.Parameters.AddWithValue("password", Verification.GetSHA512Hash(password));

                    int i = await command.ExecuteNonQueryAsync();
                    if (i == 1)
                    {
                        MessageBox.Show("Вы успешно зарегистрировались");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка записи");
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public static async Task SendEmailAsync(string email, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация пекарни", "Beggar-bro@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Логин и пароль";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 25, false);
                await client.AuthenticateAsync("Beggar-bro@yandex.ru", "vmkzcaydjlyxkbhw");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}