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
using System.Windows.Shapes;

namespace bakery.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditSaleWindow.xaml
    /// </summary>
    public partial class AddEditSaleWindow : Window
    {
        List<Sale> sale = new List<Sale>();
        public static int selectedIndex;
        public AddEditSaleWindow()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            sale = await SaleFromDb.GetSale();
            comboBoxPlan.ItemsSource = await ProductReleasePlanFromDb.GetProductReleasePlan();
            comboBoxPlan.DisplayMemberPath = "IdPlan";
            comboBoxPlan.SelectedValuePath = "IdPlan";

            comboBoxEmployee.ItemsSource = await EmployeeFromDb.GetEmployee();
            comboBoxEmployee.DisplayMemberPath = "Fio";
            comboBoxEmployee.SelectedValuePath = "IdEmployee";

        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
