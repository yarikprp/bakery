using bakery.Classes;
using bakery.Model;
using bakery.View;
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
        public static ProductReleasePlan CurrentProductReleasePlan { get; set; } = null;

        public ProductRealsePlanPage()
        {
            InitializeComponent();
        }

        private async void ProductReleasePlanPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllProductReleasePlan();
        }

        public async Task ViewAllProductReleasePlan()
        {
            productReleasePlans = await ProductReleasePlanFromDb.GetProductReleasePlan();

            dataGridProductReleasePlan.ItemsSource = productReleasePlans;
        }

        List<ProductReleasePlan> SearchProductReleasePlan(string searchString)
        {
            productReleasePlansSearch.Clear();

            foreach (ProductReleasePlan item in productReleasePlans)
            {
                if (item.Fio.StartsWith(searchString) || item.NameProduct.StartsWith(searchString))
                {
                    productReleasePlansSearch.Add(item);
                }
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
            if (dataGridProductReleasePlan.SelectedItem != null)
            {
                ProductReleasePlan selectedEdit = (ProductReleasePlan)dataGridProductReleasePlan.SelectedItem;
                string warning = "Вы действительно хотите редактировать из плана " + selectedEdit.NameProduct + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    AddEditProductRealsePlanWindow addEditProductRealsePlanWindow = new AddEditProductRealsePlanWindow();

                    addEditProductRealsePlanWindow.comboBoxProduct.Text = selectedEdit.NameProduct;
                    addEditProductRealsePlanWindow.comboBoxEmployee.Text = selectedEdit.Fio;
                    addEditProductRealsePlanWindow.datePickerRelease.Text = selectedEdit.PlannedReleaseDate.ToString();

                    AddEditProductRealsePlanWindow.selectedIndex = productReleasePlans.FindIndex(u => u.IdPlan == selectedEdit.IdPlan);

                    addEditProductRealsePlanWindow.ShowDialog();

                    dataGridProductReleasePlan.ItemsSource = null;
                    dataGridProductReleasePlan.ItemsSource = productReleasePlans;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите план для редактирования.");
            }

        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProductReleasePlan.SelectedItem != null)
            {
                ProductReleasePlan selected = (ProductReleasePlan)dataGridProductReleasePlan.SelectedItem;
                string warning = "Вы действительно хотите удалить из плана " + selected.NameProduct + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    await ProductReleasePlanFromDb.DeleteProductReleasePlan(selected);

                    productReleasePlans.Remove(selected);

                    dataGridProductReleasePlan.ItemsSource = null;
                    dataGridProductReleasePlan.ItemsSource = productReleasePlans;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите план для удаления.");
            }

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditProductRealsePlanWindow addEditProductRealsePlanWindow = new AddEditProductRealsePlanWindow();
            addEditProductRealsePlanWindow.ParentPage = this;
            addEditProductRealsePlanWindow.ShowDialog();
        }
    }
}
