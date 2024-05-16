using bakery.Classes;
using bakery.Model;
using bakery.Page;
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
    /// Логика взаимодействия для AddEditProductWindow.xaml
    /// </summary>
    public partial class AddEditProductWindow : Window
    {
        public ProductPage ParentPage { get; set; }
        List<Product> product = new List<Product>();
        public static int selectedIndex;

        public AddEditProductWindow()
        {
            InitializeComponent();
        }
        
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            product = await ProductFromDb.GetProduct();
            comboBoxEmployee.ItemsSource = await EmployeeFromDb.GetEmployee();
            comboBoxEmployee.DisplayMemberPath = "Fio";
            comboBoxEmployee.SelectedValuePath = "IdEmployee";
        }

        private async void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInput())
            {
                string product = textBoxProduct.Text;
                string employee = comboBoxEmployee.Text;
                DateTime daterealeases = DateTime.Parse(datePickerReleases.Text);
                int price = Convert.ToInt32(textBoxPrice.Text);
                DateTime datemanf = DateTime.Parse(datePickerManf.Text);
                int quantity = Convert.ToInt32(textBoxQuantity.Text);

                await ProductFromDb.AddProduct(product, employee, daterealeases, price, datemanf, quantity);
                Close();
                if (ParentPage != null)
                {
                    await ParentPage.ViewAllProduct();
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBoxProduct.Text))
            {
                MessageBox.Show("Поле 'Продукт' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBoxEmployee.Text))
            {
                MessageBox.Show("Поле 'Сотрудник' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(datePickerReleases.Text))
            {
                MessageBox.Show("Поле 'Дата реализации' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxPrice.Text))
            {
                MessageBox.Show("Поле 'Цена' не может быть пустым.");
                return false;
            }

            int price;
            if (!int.TryParse(textBoxPrice.Text, out price))
            {
                MessageBox.Show("Поле 'Цена' должно содержать только числовое значение.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(datePickerManf.Text))
            {
                MessageBox.Show("Поле 'Срок годностьи' не может быть пустым.");
                return false;
            }

            int quantity;
            if (!int.TryParse(textBoxQuantity.Text, out quantity))
            {
                MessageBox.Show("Поле 'Количество' должно содержать только числовое значение.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxQuantity.Text))
            {
                MessageBox.Show("Поле 'Количество' не может быть пустым.");
                return false;
            }

            return true;
        }
    }
}
