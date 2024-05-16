using bakery.Model;
using bakery.View;
using kulinaria_app_v2.Classes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Role = bakery.Classes.Role;

namespace bakery.Page
{
    /// <summary>
    /// Логика взаимодействия для UserManegmentPage.xaml
    /// </summary>
    public partial class UserManegmentPage : System.Windows.Controls.Page
    {
        List<User> users = new List<User>();
        List<User> usersSearch = new List<User>();
        static List<Role> role = new List<Role>();
        public int selectedIndex;
        public UserManegmentPage()
        {
            InitializeComponent();
        }

        private async void UsersControl_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllUsers();
            role = await RoleFromDb.GetRoles();
            role.Insert(0, new Role(0, "Все"));

            comboBoxRoles.ItemsSource = role;
            comboBoxRoles.DisplayMemberPath = "Name";
            comboBoxRoles.SelectedValuePath = "Id";
        }

        async Task ViewAllUsers()
        {
            users = await UserFromDb.GetUsers();

            dataGridUser.ItemsSource = users;
        }

        private async void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Hide();

            bool? res = registrationWindow.ShowDialog();
            if (res == true)
            {
                users = await UserFromDb.GetUsers();
                dataGridUser.ItemsSource = users;
            }
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUser.SelectedItem != null)
            {
                User selectedUser = (User)dataGridUser.SelectedItem;
                string warning = "Вы действительно хотите удалить пользователя " + selectedUser.FirstName + " " + selectedUser.LastName + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    if (selectedUser.UserId != AuthorisationsWindow.CurrentUser.UserId)
                    {
                        await UserFromDb.DeleteUser(selectedUser);

                        users.Remove(selectedUser);

                        dataGridUser.ItemsSource = null;
                        dataGridUser.ItemsSource = users;
                    }
                    else
                    {
                        MessageBox.Show("Извините, но вы не можете удалить себя, пока находитесь в системе");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для удаления.");
            }
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
                dataGridUser.ItemsSource = SearchUsers(txbSearchs.Text);
            }
            else
            {
                dataGridUser.ItemsSource = users;
            }
        }

        private async void comboBoxRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxRoles.SelectedIndex == 0)
            {
                await ViewAllUsers();
            }
            else
            {
                users = await UserFromDb.FilterUserByRole(comboBoxRoles.SelectedIndex);

                dataGridUser.ItemsSource = users;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUser.SelectedItem != null)
            {
                User selectedUser = (User)dataGridUser.SelectedItem;
                string warning = "Вы действительно хотите открыть редактирование пользователя " + selectedUser.FirstName + " " + selectedUser.LastName + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    if (selectedUser.UserId != AuthorisationsWindow.CurrentUser.UserId)
                    {
                        UserManegmentEdit userManegmentEdit = new UserManegmentEdit();

                        userManegmentEdit.textBoxFirstName.Text = selectedUser.FirstName;
                        userManegmentEdit.textBoxLastName.Text = selectedUser.LastName;
                        userManegmentEdit.textBoxPatronymic.Text = selectedUser.Patronymic;
                        userManegmentEdit.dateTimePickerBirthDay.SelectedDate = selectedUser.DateOfBirthday;
                        userManegmentEdit.textBoxPhone.Text = selectedUser.Phone;
                        userManegmentEdit.textBoxAdress.Text = selectedUser.Adress;
                        userManegmentEdit.textBoxEmail.Text = selectedUser.Email;

                        userManegmentEdit.selectedIndex = users.FindIndex(u => u.UserId == selectedUser.UserId);

                        userManegmentEdit.Show();
                        dataGridUser.ItemsSource = null;
                        dataGridUser.ItemsSource = users;
                    }
                    else
                    {
                        MessageBox.Show("Извините, но вы не можете удалить себя, пока находитесь в системе");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для редактирования профиля.");
            }
        }
    }
 }