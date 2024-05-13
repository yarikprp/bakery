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
    /// Логика взаимодействия для AddEditSupplierWindow.xaml
    /// </summary>
    public partial class AddEditSupplierWindow : Window
    {
        public SupplierPage ParentPage { get; set; }
        public static int selectedIndex;
        List<Supplier> supplier = new List<Supplier>();
        public AddEditSupplierWindow()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            supplier = await SupplierFromDb.GetSupplier();
            comboBoxCompany.ItemsSource = await CompanyFromDb.GetCompany();
            comboBoxCompany.DisplayMemberPath = "NameCompany";
            comboBoxCompany.SelectedValuePath = "IdCompany";
        }

        private async void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            string ingredient = textBoxFIngredient.Text;
            string nameCompany = comboBoxCompany.Text;
            await SupplierFromDb.AddSupplier(ingredient, nameCompany);

            if (ParentPage != null)
            {
                await ParentPage.ViewAllCompany();
            }
        }
    }
}
