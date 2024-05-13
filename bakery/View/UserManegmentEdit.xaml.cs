using bakery.Classes;
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
    /// Логика взаимодействия для UserManegmentEdit.xaml
    /// </summary>
    public partial class UserManegmentEdit : Window
    {
        List<User> users = new List<User>();
        public int selectedIndex;
        public UserManegmentEdit()
        {
            InitializeComponent();
        }

        private async void UserManegmentEdit_Loaded(object sender, RoutedEventArgs e)
        {
            users = await UserFromDb.GetUsers();
            ComboBoxRole.ItemsSource = await RoleFromDb.GetRoles();
            ComboBoxRole.DisplayMemberPath = "Name";
            ComboBoxRole.SelectedValuePath = "Id";
        }

        private async void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            await UserFromDb.ChangeRole(users[selectedIndex], ComboBoxRole.SelectedIndex + 1);

            users = await UserFromDb.GetUsers();
        }
    }
}
