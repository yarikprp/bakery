using bakery.Model;
using kulinaria_app_v2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для AuthorisationsWindow.xaml
    /// </summary>
    public partial class AuthorisationsWindow : Window
    {
        public static User CurrentUser { get; set; } = null;

        private int loginButtonClickCount = 0;
        public AuthorisationsWindow()
        {
            InitializeComponent();
        }

        private async void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == "" && textBoxPassword.Password == "")
            {
                MessageBox.Show("Введите данные");
                return;
            }
            else
            {
                if (loginButtonClickCount == 1)
                {
/*                    ShowCaptchaForm();
*/                }
                else
                {
                    CurrentUser = await UserFromDb.GetUser(textBoxLogin.Text, textBoxPassword.Password);

                    if (CurrentUser != null)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя не существует");
                        loginButtonClickCount++;
                    }
                }
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();

            MessageBoxResult res = MessageBox.Show("Вы точно хотите создать аккаунт?", "Создание аккаунта", MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.OK)
            {
                Show();
            }
            else
            {
                registrationWindow.Close();
            }

        }
    }
}
