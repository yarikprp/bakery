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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bakery.Page
{
    /// <summary>
    /// Логика взаимодействия для ReceiptWarehousePage.xaml
    /// </summary>
    public partial class ReceiptWarehousePage : System.Windows.Controls.Page
    {
        List<ReceiptWarehouse> receiptWarehouse = new List<ReceiptWarehouse>();
        List<ReceiptWarehouse> receiptWarehouseSearch = new List<ReceiptWarehouse>();

        public ReceiptWarehousePage()
        {
            InitializeComponent();
        }

        private async void ReceiptWarehousePage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllReceiptWarehouse();
        }

        async Task ViewAllReceiptWarehouse()
        {
            receiptWarehouse = await ReceiptWarehouseFromDb.GetReceiptWarehouse();

            dataGridReceiptWarehouses.ItemsSource = receiptWarehouse;
        }

        List<ReceiptWarehouse> SearchReceiptWarehouse(string searchString)
        {
            receiptWarehouseSearch.Clear();

            foreach (ReceiptWarehouse item in receiptWarehouse)
            {
               /* if (item.Fio.StartsWith(searchString))
                {
                    receiptWarehouseSearch.Add(item);
                }*/
            }

            return receiptWarehouseSearch;
        }


        private void txbSearchs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbSearchs.Text.Length != 0)
            {
                dataGridReceiptWarehouses.ItemsSource = SearchReceiptWarehouse(txbSearchs.Text);
            }
            else
            {
                dataGridReceiptWarehouses.ItemsSource = receiptWarehouse;
            }
        }

        private void comboBoxReceiptWarehouses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridReceiptWarehouses.SelectedItem != null)
            {
                ReceiptWarehouse selected = (ReceiptWarehouse)dataGridReceiptWarehouses.SelectedItem;
                string warning = "Вы действительно хотите удалить товар?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    await ReceiptWarehouseFromDb.DeleteReceiptWarehouse(selected);

                    receiptWarehouse.Remove(selected);

                    dataGridReceiptWarehouses.ItemsSource = null;
                    dataGridReceiptWarehouses.ItemsSource = receiptWarehouse;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите товар для удаления.");
            }

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
