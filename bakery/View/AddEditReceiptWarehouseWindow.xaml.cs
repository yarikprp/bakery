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
    /// Логика взаимодействия для AddEditReceiptWarehouseWindow.xaml
    /// </summary>
    public partial class AddEditReceiptWarehouseWindow : Window
    {
        List<ReceiptWarehouse> receiptWarehouses = new List<ReceiptWarehouse>();
        public static int selectedIndex;
        public ReceiptWarehousePage ParentPage { get; set; }
        public AddEditReceiptWarehouseWindow()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            receiptWarehouses = await ReceiptWarehouseFromDb.GetReceiptWarehouse();
            comboBoxIngredients.ItemsSource = await IngredientsFromDb.GetIngredients();
            comboBoxIngredients.DisplayMemberPath = "NameIngredients";
            comboBoxIngredients.SelectedValuePath = "IdIngredients";

            comboBoxProduct.ItemsSource = await ProductFromDb.GetProduct();
            comboBoxProduct.DisplayMemberPath = "NameProduct";
            comboBoxProduct.SelectedValuePath = "IdProduct";

            comboBoxSupplier.ItemsSource = await SupplierFromDb.GetSupplier();
            comboBoxSupplier.DisplayMemberPath = "IdSupplier";
            comboBoxSupplier.SelectedValuePath = "IdSupplier";

            if (ReceiptWarehousePage.CurrentReceiptWarehouse != null)
            {
                comboBoxIngredients.Text = ReceiptWarehousePage.CurrentReceiptWarehouse.NameIngredients;
                comboBoxProduct.Text = ReceiptWarehousePage.CurrentReceiptWarehouse.NameProduct;
                comboBoxSupplier.Text = ReceiptWarehousePage.CurrentReceiptWarehouse.NameCompany;
                dateTimeDateOfReceipt.Text = ReceiptWarehousePage.CurrentReceiptWarehouse.DateOfReceipt.ToString();
                textBoxQuantity.Text = ReceiptWarehousePage.CurrentReceiptWarehouse.Quantity.ToString();
            }
        }
        
        private async void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInput())
            {
                if (ReceiptWarehousePage.CurrentReceiptWarehouse != null)
                {
                    ReceiptWarehousePage.CurrentReceiptWarehouse.NameIngredients = comboBoxIngredients.Text;
                    ReceiptWarehousePage.CurrentReceiptWarehouse.NameProduct = comboBoxProduct.Text;
                    ReceiptWarehousePage.CurrentReceiptWarehouse.NameCompany = comboBoxSupplier.Text;
                    ReceiptWarehousePage.CurrentReceiptWarehouse.DateOfReceipt = DateTime.Parse(dateTimeDateOfReceipt.Text);
                    ReceiptWarehousePage.CurrentReceiptWarehouse.Quantity = textBoxQuantity.Text;

                    await ReceiptWarehouseFromDb.UpdateReceiptWarehouse(ReceiptWarehousePage.CurrentReceiptWarehouse);

                    Close();
                }
                else
                {
                    string ingredient = comboBoxIngredients.Text;
                    string product = comboBoxProduct.Text;
                    int company = Convert.ToInt32(comboBoxSupplier.Text);
                    DateTime date = DateTime.Parse(dateTimeDateOfReceipt.Text);
                    int quantitys = Convert.ToInt32(textBoxQuantity.Text);

                    await ReceiptWarehouseFromDb.AddReceiptWarehouse(ingredient, product, company, date, quantitys);

                    Close();
                }

                if (ParentPage != null)
                {
                    await ParentPage.ViewAllReceiptWarehouse();
                }

            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(comboBoxIngredients.Text))
            {
                MessageBox.Show("Поле 'Ингредиент' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBoxProduct.Text))
            {
                MessageBox.Show("Поле 'Продукт' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBoxSupplier.Text))
            {
                MessageBox.Show("Поле 'Поставщик' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(dateTimeDateOfReceipt.Text))
            {
                MessageBox.Show("Поле 'Дата' не может быть пустым.");
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
