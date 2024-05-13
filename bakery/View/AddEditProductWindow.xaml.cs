using bakery.Classes;
using bakery.Model;
using kulinaria_app_v2.Classes;
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
    /// Логика взаимодействия для AddEditProductWindow.xaml
    /// </summary>
    public partial class AddEditProductWindow : Window
    {
        List<Product> product = new List<Product>();
        public static int selectedIndex;

        public AddEditProductWindow()
        {
            InitializeComponent();
        }
        
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            product = await ProductFromDb.GetProduct();
            comboBoxEmployee.ItemsSource = await EmployeeFromDb.GetEmployee();
            comboBoxEmployee.DisplayMemberPath = "Fio";
            comboBoxEmployee.SelectedValuePath = "IdEmployee";
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
