using bakery.Classes;
using bakery.Model;
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
    /// Логика взаимодействия для PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {
        public PasswordChangeWindow()
        {
            InitializeComponent();
        }

        private async void buttonChange(object sender, RoutedEventArgs e)
        {
            if (textBoxOld.Text != "" && textBoxNew.Text != "" && textBoxPassRepeat.Text != "")
            {
                if (textBoxOld.Text != textBoxNew.Text)
                {
                    if (Verification.GetSHA512Hash(textBoxOld.Text) == AuthorisationsWindow.CurrentUser.Password)
                    {
                        if (UserFromDb.CheckPassword(textBoxNew.Text, textBoxPassRepeat.Text))
                        {
                            await UserFromDb.ChangePassword(AuthorisationsWindow.CurrentUser.UserId, textBoxNew.Text);

                            DialogResult = true;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Проверьте пароль");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Проверьте поле старого пароля");
                    }
                }
                else
                {
                    MessageBox.Show("Пароли совпадают");
                }
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }
    }
}
