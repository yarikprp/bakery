using bakery.Classes;
using bakery.Model;
using bakery.Page;
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
        public EmployeePage ParentPage { get; set; }

        List<Employee> employee = new List<Employee>();

        public static int selectedIndex;

        public AddEditEmployee()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            employee = await EmployeeFromDb.GetEmployee();
            comboBoxPost.ItemsSource = await PostFromDb.GetPost();
            comboBoxPost.DisplayMemberPath = "PostName";
            comboBoxPost.SelectedValuePath = "PostId";

            if (EmployeePage.CurrentEmployee != null)
            {
                textBoxFIO.Text = EmployeePage.CurrentEmployee.Fio;
                comboBoxPost.Text = EmployeePage.CurrentEmployee.PostName;
                textBoxMoney.Text = EmployeePage.CurrentEmployee.Salary.ToString();
                dateTimeDateOfEmploymentPickerBirthDay.Text = EmployeePage.CurrentEmployee.DateOfEmployment.ToString();
            }
        }

        private async void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInput())
            {
                if (EmployeePage.CurrentEmployee != null)
                {
                    EmployeePage.CurrentEmployee.Fio = textBoxFIO.Text;
                    EmployeePage.CurrentEmployee.PostName = comboBoxPost.Text;
                    EmployeePage.CurrentEmployee.Salary = textBoxMoney.Text;
                    EmployeePage.CurrentEmployee.DateOfEmployment = DateTime.Parse(dateTimeDateOfEmploymentPickerBirthDay.Text);

                    await EmployeeFromDb.UpdateEmployee(EmployeePage.CurrentEmployee);

                    Close();
                }
                else
                {
                    string fio = textBoxFIO.Text;
                    string postName = comboBoxPost.Text;
                    string salary = textBoxMoney.Text;
                    DateTime date = DateTime.Parse(dateTimeDateOfEmploymentPickerBirthDay.Text);

                    await EmployeeFromDb.AddEmployee(fio, postName, salary, date);

                    Close();
                }

                if (ParentPage != null)
                {
                    await ParentPage.ViewAllEmployee();
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBoxFIO.Text))
            {
                MessageBox.Show("Поле 'ФИО' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBoxPost.Text))
            {
                MessageBox.Show("Поле 'Должность' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxMoney.Text))
            {
                MessageBox.Show("Поле 'Цена' не может быть пустым.");
                return false;
            }

            double money;
            if (!double.TryParse(textBoxMoney.Text, out money))
            {
                MessageBox.Show("Поле 'Цена' должно содержать только числовое значение.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(dateTimeDateOfEmploymentPickerBirthDay.Text))
            {
                MessageBox.Show("Поле 'Дата' не может быть пустым.");
                return false;
            }

            return true;
        }
    }
}
