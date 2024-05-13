using bakery.Classes;
using bakery.Model;
using bakery.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public CompanyPage()
        {
            InitializeComponent();
        }

        private async void CompanyPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllCompany();

            comboBoxCompany.DisplayMemberPath = "companyName";
            comboBoxCompany.Items.Add("Company");
            comboBoxCompany.Items.Add("Fio");
            comboBoxCompany.Items.Add("NamePhone");
            comboBoxCompany.Items.Add("Adress");
        }

        async Task ViewAllCompany()
        {
            company = await CompanyFromDb.GetCompany();

            dataGridCompany.ItemsSource = company;
        }

        private void SortDataGrid(string sortBy, ListSortDirection direction)
        {
            var dataView = CollectionViewSource.GetDefaultView(dataGridCompany.ItemsSource);
            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
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

        private void comboBoxCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxCompany.SelectedItem != null)
            {
                string selectedSortBy = (comboBoxCompany.SelectedItem).ToString();
                ListSortDirection direction = ListSortDirection.Ascending;

                SortDataGrid(selectedSortBy, direction);
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

                    addEditCompanyWindow.textBoxCompany.Text = selectedEdit.NameCompany;
                    addEditCompanyWindow.textBoxFio.Text = selectedEdit.Fio;
                    addEditCompanyWindow.textBoxPhone.Text = selectedEdit.NamePhone;
                    addEditCompanyWindow.textBoxAdress.Text = selectedEdit.Adress.ToString();

                    AddEditCompanyWindow.selectedIndex = company.FindIndex(u => u.IdCompany == selectedEdit.IdCompany);

                    addEditCompanyWindow.Show();

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
            addEditCompanyWindow.Show();
        }
    }
}