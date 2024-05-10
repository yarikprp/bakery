using bakery.Classes;
using bakery.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace bakery.Page
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : System.Windows.Controls.Page
    {
        List<Employee> employee = new List<Employee>();
        public EmployeePage()
        {
            InitializeComponent();
        }

        private async void EmployeePage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllUsers();/*
            role = await RoleFromDb.GetRoles();
            role.Insert(0, new Role(0, "Все"));*/
            /*
                        comboBoxRoles.ItemsSource = role;
                        comboBoxRoles.DisplayMemberPath = "RoleName";
                        comboBoxRoles.SelectedValuePath = "RoleId";
                    }*/
        }

        async Task ViewAllUsers()
        {
            employee = await EmployeeFromDb.GetEmployee();

            dataGridEmployee.ItemsSource = employee;
        }

        private void txbSearchs_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void comboBoxPost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
