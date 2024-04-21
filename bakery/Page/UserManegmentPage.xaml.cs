using bakery.Model;
using bakery.View;
using kulinaria_app_v2.Classes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bakery.Page
{
    /// <summary>
    /// Логика взаимодействия для UserManegmentPage.xaml
    /// </summary>
    public partial class UserManegmentPage : System.Windows.Controls.Page
    {
        List<User> users = new List<User>();
        List<User> usersSearch = new List<User>();
        int selectedIndex;

        public UserManegmentPage()
        {
            InitializeComponent();
        }

        private async void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Hide();

            bool? res = registrationWindow.ShowDialog();
            if (res == true)
            {
                users = await UserFromDb.GetUsers();
                listViewUser.ItemsSource = users;
            }
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            string warning = "Вы действительно хотите удалить пользователя " + users[selectedIndex].FirstName + " " + users[selectedIndex].LastName + "?";

            MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.OK)
            {
                if (users[selectedIndex].UserId != AuthorisationsWindow.CurrentUser.UserId)
                {
                    await UserFromDb.DeleteUser(users[selectedIndex]);

                    users = await UserFromDb.GetUsers();
                    listViewUser.ItemsSource = users;
                }
                else
                {
                    MessageBox.Show("Извините, но вы не можете удалить себя, пока находитесь в системе");
                }
            }
        }

        private async void buttonChangeRole_Click(object sender, RoutedEventArgs e)
        {
            await UserFromDb.ChangeRole(users[selectedIndex], comboBoxRoles.SelectedIndex + 1);

            users = await UserFromDb.GetUsers();
            listViewUser.ItemsSource = users;
        }

        List<User> SearchUsers(string searchString)
        {
            usersSearch.Clear();

            foreach (User item in users)
            {
                if (item.LastName.StartsWith(searchString) || item.FirstName.StartsWith(searchString))
                {
                    usersSearch.Add(item);
                }
            }

            return usersSearch;
        }


        private void txbSearchs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbSearchs.Text.Length != 0)
            {
                listViewUser.ItemsSource = SearchUsers(txbSearchs.Text);
            }
            else
            {
                listViewUser.ItemsSource = users;
            }

        }

        private async void UsersControlForm_Loaded(object sender, RoutedEventArgs e)
        {
            users = await UserFromDb.GetUsers();
            listViewUser.ItemsSource = users;
            comboBoxRoles.ItemsSource = await RoleFromDb.GetRoles();
        }
    }
}