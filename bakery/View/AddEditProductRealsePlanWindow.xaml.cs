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
    /// Логика взаимодействия для AddEditProductRealsePlanWindow.xaml
    /// </summary>
    public partial class AddEditProductRealsePlanWindow : Window
    {
        List<ReceiptWarehouse> receiptWarehouses = new List<ReceiptWarehouse>();
        public static int selectedIndex;
        public AddEditProductRealsePlanWindow()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            receiptWarehouses = await ReceiptWarehouseFromDb.GetReceiptWarehouse();
            comboBoxProduct.ItemsSource = await ProductFromDb.GetProduct();
            comboBoxProduct.DisplayMemberPath = "NameProduct";
            comboBoxProduct.SelectedValuePath = "IdProduct";

            comboBoxEmployee.ItemsSource = await EmployeeFromDb.GetEmployee();
            comboBoxEmployee.DisplayMemberPath = "Fio";
            comboBoxEmployee.SelectedValuePath = "IdEmployee";
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
