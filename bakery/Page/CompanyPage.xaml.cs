using bakery.Classes;
using bakery.Model;
using bakery.View;
using kulinaria_app_v2.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class CompanyPage : System.Windows.Controls.Page
    {
        List<Company> company = new List<Company>();
        List<Company> companySearch = new List<Company>();
        public static Company CurrentCompany { get; set; } = null;

        public CompanyPage()
        {
            InitializeComponent();
        }

        private async void CompanyPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllCompany();
            company = await CompanyFromDb.GetCompany();

            comboBoxCompany.ItemsSource = company;
            comboBoxCompany.DisplayMemberPath = "NameCompany";
            comboBoxCompany.SelectedValuePath = "IdCompany";

        }

       public async Task ViewAllCompany()
       {
            company = await CompanyFromDb.GetCompany();

            dataGridCompany.ItemsSource = company;
       }

        List<Company> SearchSupplier(string searchString)
        {
            companySearch.Clear();

            foreach (Company item in company)
            {
                if (item.NameCompany.StartsWith(searchString) || item.Fio.StartsWith(searchString) || item.Adress.StartsWith(searchString))
                {
                    companySearch.Add(item);
                }
            }
            return companySearch;
        }

        private void txbSearchs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbSearchs.Text.Length != 0)
            {
                dataGridCompany.ItemsSource = SearchSupplier(txbSearchs.Text);
            }
            else
            {
                dataGridCompany.ItemsSource = company;
            }
        }

        private async void comboBoxCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxCompany.SelectedIndex == 0)
            {
                await ViewAllCompany();
            }
            else
            {
                company = await CompanyFromDb.FilterCompanuById(comboBoxCompany.SelectedIndex);

                dataGridCompany.ItemsSource = company;
            }
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCompany.SelectedItem != null)
            {
                Company selectedEdit = (Company)dataGridCompany.SelectedItem;
                string warning = "Вы действительно хотите редактировать " + selectedEdit.NameCompany + " ?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    AddEditCompanyWindow addEditCompanyWindow = new AddEditCompanyWindow();

                    CurrentCompany = (Company)dataGridCompany.SelectedItem;

                    addEditCompanyWindow.ShowDialog();

                    dataGridCompany.ItemsSource = null;
                    dataGridCompany.ItemsSource = company;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите компанию для редактирования.");
            }

        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCompany.SelectedItem != null)
            {
                Company selected = (Company)dataGridCompany.SelectedItem;
                string warning = "Вы действительно хотите удалить " + selected.NameCompany + " ?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    await CompanyFromDb.DeleteCompany(selected);

                    company.Remove(selected);

                    dataGridCompany.ItemsSource = null;
                    dataGridCompany.ItemsSource = company;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите компанию для удаления.");
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditCompanyWindow addEditCompanyWindow = new AddEditCompanyWindow();
            addEditCompanyWindow.ParentPage = this;
            addEditCompanyWindow.ShowDialog();
        }
    }
}