using bakery.Classes;
using bakery.Model;
using bakery.View;
using kulinaria_app_v2.Classes;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bakery.Page
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : System.Windows.Controls.Page
    {
        List<Product> product = new List<Product>();
        List<Product> productSearch = new List<Product>(); 
        public static Product CurrentProduct { get; set; } = null;
        
        public ProductPage()
        {
            InitializeComponent();
        }

        private async void ProductPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllProduct();
            product = await ProductFromDb.GetProduct();

            comboBoxProduct.ItemsSource = product;
            comboBoxProduct.DisplayMemberPath = "NameProduct";
            comboBoxProduct.SelectedValuePath = "IdProduct";
        }

        public async Task ViewAllProduct()
        {
            product = await ProductFromDb.GetProduct();

            dataGridProduct.ItemsSource = product;
        }

        List<Product> SearchProduct(string searchString)
        {
            productSearch.Clear();

            foreach (Product item in product)
            {
                if (item.NameProduct.StartsWith(searchString))
                {
                    productSearch.Add(item);
                }
            }

            return productSearch;
        }


        private void txbSearchs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbSearchs.Text.Length != 0)
            {
                dataGridProduct.ItemsSource = SearchProduct(txbSearchs.Text);
            }
            else
            {
                dataGridProduct.ItemsSource = product;
            }
        }

        private async void comboBoxProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxProduct.SelectedIndex == 0)
            {
                await ViewAllProduct();
            }
            else
            {
                product = await ProductFromDb.FilterProductByEmployee(comboBoxProduct.SelectedIndex);

                dataGridProduct.ItemsSource = product;
            }
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProduct.SelectedItem != null)
            {
                Product selectedEdit = (Product)dataGridProduct.SelectedItem;
                string warning = "Вы действительно хотите редактировать " + selectedEdit.NameProduct + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    AddEditProductWindow addEditProductWindow = new AddEditProductWindow();

                    CurrentProduct = (Product)dataGridProduct.SelectedItem; 
                    addEditProductWindow.ParentPage = this;


                    addEditProductWindow.ShowDialog();


                    dataGridProduct.ItemsSource = null;
                    dataGridProduct.ItemsSource = product;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт для редактирования.");
            }


        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProduct.SelectedItem != null)
            {
                Product selected = (Product)dataGridProduct.SelectedItem;
                string warning = "Вы действительно хотите удалить " + selected.NameProduct + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    await ProductFromDb.DeleteProduct(selected);

                    product.Remove(selected);

                    dataGridProduct.ItemsSource = null;
                    dataGridProduct.ItemsSource = product;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт для удаления.");
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditProductWindow addEditProductWindow = new AddEditProductWindow();
            addEditProductWindow.ParentPage = this;
            addEditProductWindow.ShowDialog();
        }
    }
}
