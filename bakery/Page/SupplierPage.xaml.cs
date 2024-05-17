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
    /// Логика взаимодействия для SupplierPage.xaml
    /// </summary>
    public partial class SupplierPage : System.Windows.Controls.Page
    {
        List<Supplier> supplier = new List<Supplier>();
        List<Supplier> supplierSearch = new List<Supplier>();
        static List<Company> companies = new List<Company>();
        public static Supplier CurrentSupplier { get; set; } = null;

        public SupplierPage()
        {
            InitializeComponent();
        }

        private async void CompanyPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllCompany();
            companies = await CompanyFromDb.GetCompany();
/*            companies.Insert(0, new Company(0, "Все"));
*/
            comboBoxCompany.ItemsSource = companies;
            comboBoxCompany.DisplayMemberPath = "NameCompany";
            comboBoxCompany.SelectedValuePath = "IdCompany";
        }

        public async Task ViewAllCompany()
        {
            supplier = await SupplierFromDb.GetSupplier();

            dataGridCompany.ItemsSource = supplier;
        }

        List<Supplier> SearchSupplier(string searchString)
        {
            supplierSearch.Clear();

            foreach (Supplier item in supplier)
            {
                if (item.Ingredient.StartsWith(searchString))
                {
                    supplierSearch.Add(item);
                }
            }

            return supplierSearch;
        }


        private void txbSearchs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbSearchs.Text.Length != 0)
            {
                dataGridCompany.ItemsSource = SearchSupplier(txbSearchs.Text);
            }
            else
            {
                dataGridCompany.ItemsSource = supplier;
            }
        }

        private void comboBoxPost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCompany.SelectedItem != null)
            {
                Supplier selectedEdit = (Supplier)dataGridCompany.SelectedItem;
                string warning = "Вы действительно хотите редактировать поставщика " + selectedEdit.NameCompany + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    AddEditSupplierWindow addEditSupplierWindow = new AddEditSupplierWindow(); 
                    
                    CurrentSupplier = (Supplier)dataGridCompany.SelectedItem;


                    addEditSupplierWindow.ShowDialog();

                    dataGridCompany.ItemsSource = null;
                    dataGridCompany.ItemsSource = supplier;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите поставщика для редактирования.");
            }

        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCompany.SelectedItem != null)
            {
                Supplier selected = (Supplier)dataGridCompany.SelectedItem;
                string warning = "Вы действительно хотите удалить поставщика " + selected.NameCompany + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    await SupplierFromDb.DeleteSupplier(selected);

                    supplier.Remove(selected);

                    dataGridCompany.ItemsSource = null;
                    dataGridCompany.ItemsSource = supplier;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите поставщика для удаления.");
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditSupplierWindow addEditSupplierWindow = new AddEditSupplierWindow(); 
            addEditSupplierWindow.ParentPage = this;
            addEditSupplierWindow.ShowDialog();
        }
    }
}
