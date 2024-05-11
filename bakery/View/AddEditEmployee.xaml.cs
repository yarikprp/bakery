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
    /// Логика взаимодействия для AddEditEmployee.xaml
    /// </summary>
    public partial class AddEditEmployee : Window
    {
        List<Employee> employee = new List<Employee>();

        public static int selectedIndex;

        public AddEditEmployee()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            employee = await EmployeeFromDb.GetEmployee();
            comboBoxPost.ItemsSource = await RoleFromDb.GetRoles();
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
