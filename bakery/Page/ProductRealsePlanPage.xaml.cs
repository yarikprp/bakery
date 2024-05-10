using bakery.Classes;
using bakery.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bakery.Page
{
    /// <summary>
    /// Логика взаимодействия для ProductRealsePlanPage.xaml
    /// </summary>
    public partial class ProductRealsePlanPage : System.Windows.Controls.Page
    {
        List<ProductReleasePlan> productReleasePlans = new List<ProductReleasePlan>();
        List<ProductReleasePlan> productReleasePlansSearch = new List<ProductReleasePlan>();

        public ProductRealsePlanPage()
        {
            InitializeComponent();
        }

        private async void ProductReleasePlanPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllProductReleasePlan();
        }

        async Task ViewAllProductReleasePlan()
        {
            productReleasePlans = await ProductReleasePlanFromDb.GetProductReleasePlan();

            dataGridProductReleasePlan.ItemsSource = productReleasePlans;
        }

        List<ProductReleasePlan> SearchProductReleasePlan(string searchString)
        {
            productReleasePlansSearch.Clear();

            foreach (ProductReleasePlan item in productReleasePlans)
            {
                /*if (item.Fio.StartsWith(searchString))
                {
                    productReleasePlansSearch.Add(item);
                }*/
            }

            return productReleasePlansSearch;
        }


        private void txbSearchs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbSearchs.Text.Length != 0)
            {
                dataGridProductReleasePlan.ItemsSource = SearchProductReleasePlan(txbSearchs.Text);
            }
            else
            {
                dataGridProductReleasePlan.ItemsSource = productReleasePlans;
            }
        }

        private void comboBoxProductReleasePlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
