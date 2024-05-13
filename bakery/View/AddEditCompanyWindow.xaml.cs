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
    public partial class AddEditCompanyWindow : Window
    {
        public CompanyPage ParentPage { get; set; }
        Company company;

        public static int selectedIndex;
        public AddEditCompanyWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (company != null)
            {
                await CompanyFromDb.UpdateCompany(company);
            }
            else
            {
                string nameCompany = textBoxCompany.Text;
                string fio = textBoxFio.Text;
                string namePhone = textBoxPhone.Text;
                string adress = textBoxAdress.Text;
                await CompanyFromDb.AddCompany(nameCompany, fio, namePhone, adress);
            }

            if (ParentPage != null)
            {
                await ParentPage.ViewAllCompany();
            }

            this.Close();
        }
    }
}
