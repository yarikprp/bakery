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
        public Company company;

        public static int selectedIndex;
        public AddEditCompanyWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (CompanyPage.CurrentCompany != null)
            {
                textBoxCompany.Text = CompanyPage.CurrentCompany.NameCompany;
                textBoxFio.Text = CompanyPage.CurrentCompany.Fio;
                textBoxPhone.Text = CompanyPage.CurrentCompany.NamePhone;
                textBoxAdress.Text = CompanyPage.CurrentCompany.Adress;
            }
        }

        private async void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CompanyPage.CurrentCompany != null)
            {
                CompanyPage.CurrentCompany.NameCompany = textBoxCompany.Text;
                CompanyPage.CurrentCompany.Fio = textBoxFio.Text;
                CompanyPage.CurrentCompany.NamePhone = textBoxPhone.Text;
                CompanyPage.CurrentCompany.Adress = textBoxAdress.Text;

                await CompanyFromDb.UpdateCompany(CompanyPage.CurrentCompany);

                Close();
            }
            else
            {
                string nameCompany = textBoxCompany.Text;
                string fio = textBoxFio.Text;
                string namePhone = textBoxPhone.Text;
                string adress = textBoxAdress.Text;

                await CompanyFromDb.AddCompany(nameCompany, fio, namePhone, adress);

                Close();
            }


            if (ParentPage != null)
            {
                await ParentPage.ViewAllCompany();
            }
        }
    }
}
