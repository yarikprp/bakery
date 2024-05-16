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
    /// Логика взаимодействия для AddEditSaleWindow.xaml
    /// </summary>
    public partial class AddEditSaleWindow : Window
    {
        public SalePage ParentPage { get; set; }
        List<Sale> sale = new List<Sale>();
        public static int selectedIndex;
        public AddEditSaleWindow()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            sale = await SaleFromDb.GetSale();
            comboBoxPlan.ItemsSource = await ProductReleasePlanFromDb.GetProductReleasePlan();
            comboBoxPlan.DisplayMemberPath = "IdPlan";
            comboBoxPlan.SelectedValuePath = "IdPlan";

            comboBoxEmployee.ItemsSource = await EmployeeFromDb.GetEmployee();
            comboBoxEmployee.DisplayMemberPath = "Fio";
            comboBoxEmployee.SelectedValuePath = "IdEmployee";

        }

        private async void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                int plan = Convert.ToInt32(comboBoxPlan.Text);
                string employee = comboBoxEmployee.Text;
                DateTime dateSale = DateTime.Parse(dateTimeSale.Text);
                int quantitys = Convert.ToInt32(textBoxQuantity.Text);
                await SaleFromDb.AddSale(plan, employee, dateSale, quantitys);
                Close();
                if (ParentPage != null)
                {
                    await ParentPage.ViewAllEmployee();
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(comboBoxPlan.Text))
            {
                MessageBox.Show("Поле 'План' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBoxEmployee.Text))
            {
                MessageBox.Show("Поле 'Сотрудник' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxQuantity.Text))
            {
                MessageBox.Show("Поле 'Количество' не может быть пустым.");
                return false;
            }

            int money;
            if (!int.TryParse(textBoxQuantity.Text, out money))
            {
                MessageBox.Show("Поле 'Количество' должно содержать только числовое значение.");
                return false;
            }

            return true;
        }
    }
}
