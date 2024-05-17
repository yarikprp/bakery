using bakery.Classes;
using bakery.Model;
using bakery.View;
using kulinaria_app_v2.Classes;
using System.Collections.Generic;
using System.Data;
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
        List<Employee> employeeSearch = new List<Employee>();
        static List<Post> post = new List<Post>();
        public static Employee CurrentEmployee { get; set; } = null;

        public EmployeePage()
        {
            InitializeComponent();
        }

        private async void EmployeePage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllEmployee();
            post = await PostFromDb.GetPost();
            post.Insert(0, new Post(0, "Все"));

            comboBoxPost.ItemsSource = post;
            comboBoxPost.DisplayMemberPath = "PostName";
            comboBoxPost.SelectedValuePath = "PostId";
        }

        public async Task ViewAllEmployee()
        {
            employee = await EmployeeFromDb.GetEmployee();

            dataGridEmployee.ItemsSource = employee;
        }

        List<Employee> SearchEmployee(string searchString)
        {
            employeeSearch.Clear();

            foreach (Employee item in employee)
            {
                if (item.Fio.StartsWith(searchString))
                {
                    employeeSearch.Add(item);
                }
            }

            return employeeSearch;
        }


        private void txbSearchs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbSearchs.Text.Length != 0)
            {
                dataGridEmployee.ItemsSource = SearchEmployee(txbSearchs.Text);
            }
            else
            {
                dataGridEmployee.ItemsSource = employee;
            }
        }

        private async void comboBoxPost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxPost.SelectedIndex == 0)
            {
                await ViewAllEmployee();
            }
            else
            {
                employee = await EmployeeFromDb.FilterEmployeeByPost(comboBoxPost.SelectedIndex);

                dataGridEmployee.ItemsSource = employee;
            }
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridEmployee.SelectedItem != null)
            {
                Employee selected = (Employee)dataGridEmployee.SelectedItem;
                string warning = "Вы действительно хотите удалить " + selected.Fio + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    await EmployeeFromDb.DeleteEmployee(selected);

                    employee.Remove(selected);

                    dataGridEmployee.ItemsSource = null;
                    dataGridEmployee.ItemsSource = employee;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите сотрудника для удаления.");
            }

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditEmployee addEditEmployee = new AddEditEmployee();
            addEditEmployee.ParentPage = this;
            addEditEmployee.ShowDialog();
        }

        private  void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridEmployee.SelectedItem != null)
            {
                Employee selectedEdit = (Employee)dataGridEmployee.SelectedItem;
                string warning = "Вы действительно хотите открыть редактирование сотрудника " + selectedEdit.Fio + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    AddEditEmployee addEditEmployee = new AddEditEmployee();

                    addEditEmployee.textBoxFIO.Text = selectedEdit.Fio;
                    addEditEmployee.comboBoxPost.Text = selectedEdit.PostName;
                    addEditEmployee.textBoxMoney.Text = selectedEdit.Salary.ToString();
                    addEditEmployee.dateTimeDateOfEmploymentPickerBirthDay.SelectedDate = selectedEdit.DateOfEmployment;

                    AddEditEmployee.selectedIndex = employee.FindIndex(u => u.IdEmployee == selectedEdit.IdEmployee);

                    addEditEmployee.ShowDialog();
                    dataGridEmployee.ItemsSource = null;
                    dataGridEmployee.ItemsSource = employee;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите сотрудника для редактирования.");
            }

        }
    }
}
