using bakery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace bakery.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private string GeneratePassword()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^";
            Random random = new Random();
            string password = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return password;
        }


        private void generatePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string password = GeneratePassword();
            textBoxPassword.Text = password;
        }

        private async void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxFirstName.Text == "" || textBoxLastName.Text == "" || textBoxLogin.Text == ""
                || textBoxPassword.Text == "" || textBoxPasswordRepeat.Text == "" || textBoxEmail.Text == "")
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            else
            {
                if (!IsValidEmail(textBoxEmail.Text))
                {
                    MessageBox.Show("Некорректный адрес электронной почты");
                    return;
                }

                if (await UserFromDb.CheckUser(textBoxLogin.Text) && UserFromDb.CheckPassword(textBoxPassword.Text, textBoxPasswordRepeat.Text))
                {
                    await UserFromDb.AddUser(textBoxLogin.Text, textBoxPassword.Text, textBoxFirstName.Text, textBoxLastName.Text, textBoxEmail.Text);

                    await UserFromDb.SendEmailAsync(textBoxEmail.Text, textBoxPassword.Text + textBoxLogin);

                    Close();
                }
                else
                {
                    Close();
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
