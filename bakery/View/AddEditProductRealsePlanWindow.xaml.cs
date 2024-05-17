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
    /// Логика взаимодействия для AddEditProductRealsePlanWindow.xaml
    /// </summary>
    public partial class AddEditProductRealsePlanWindow : Window
    {
        public ProductRealsePlanPage ParentPage { get; set; }
        List<ReceiptWarehouse> receiptWarehouses = new List<ReceiptWarehouse>();
        public static int selectedIndex;
        public AddEditProductRealsePlanWindow()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            receiptWarehouses = await ReceiptWarehouseFromDb.GetReceiptWarehouse();
            comboBoxProduct.ItemsSource = await ProductFromDb.GetProduct();
            comboBoxProduct.DisplayMemberPath = "NameProduct";
            comboBoxProduct.SelectedValuePath = "IdProduct";

            comboBoxEmployee.ItemsSource = await EmployeeFromDb.GetEmployee();
            comboBoxEmployee.DisplayMemberPath = "Fio";
            comboBoxEmployee.SelectedValuePath = "IdEmployee";

            if (ProductRealsePlanPage.CurrentProductReleasePlan != null)
            {
                comboBoxProduct.Text = ProductRealsePlanPage.CurrentProductReleasePlan.NameProduct;
                comboBoxEmployee.Text = ProductRealsePlanPage.CurrentProductReleasePlan.Fio;
                datePickerRelease.Text = ProductRealsePlanPage.CurrentProductReleasePlan.PlannedReleaseDate.ToString();
            }
        }
        
        private async void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInput())
            {
                if (ProductRealsePlanPage.CurrentProductReleasePlan != null)
                {
                    ProductRealsePlanPage.CurrentProductReleasePlan.NameProduct = comboBoxProduct.Text;
                    ProductRealsePlanPage.CurrentProductReleasePlan.Fio = comboBoxEmployee.Text;
                    ProductRealsePlanPage.CurrentProductReleasePlan.PlannedReleaseDate = DateTime.Parse(datePickerRelease.Text);

                    await ProductReleasePlanFromDb.UpdateProductReleasePlan(ProductRealsePlanPage.CurrentProductReleasePlan);

                    Close();
                }
                else
                {
                    string product = comboBoxProduct.Text;
                    string fio = comboBoxEmployee.Text;
                    DateTime date = DateTime.Parse(datePickerRelease.Text);
                    await ProductReleasePlanFromDb.AddProductReleasePlan(product, fio, date);

                    Close();
                }

                if (ParentPage != null)
                {
                    await ParentPage.ViewAllProductReleasePlan();
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(comboBoxProduct.Text))
            {
                MessageBox.Show("Поле 'Продукт' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBoxEmployee.Text))
            {
                MessageBox.Show("Поле 'Сотрудник' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(datePickerRelease.Text))
            {
                MessageBox.Show("Поле 'Дата реализации' не может быть пустым.");
                return false;
            }

            return true;
        }
    }
}
