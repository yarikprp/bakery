﻿using bakery.Classes;
using bakery.Model;
using bakery.View;
using kulinaria_app_v2.Classes;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Логика взаимодействия для SalePage.xaml
    /// </summary>
    public partial class SalePage : System.Windows.Controls.Page
    {
        List<Sale> sale = new List<Sale>();
        List<Sale> saleSearch = new List<Sale>(); 
        List<ProductReleasePlan> productReleasePlans = new List<ProductReleasePlan>();
        public static Sale CurrentSale { get; set; } = null;

        public SalePage()
        {
            InitializeComponent();
        }

        private async void SalePage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllEmployee();

            productReleasePlans = await ProductReleasePlanFromDb.GetProductReleasePlan();

            comboBoxSale.ItemsSource = productReleasePlans;
            comboBoxSale.DisplayMemberPath = "IdPlan";
            comboBoxSale.SelectedValuePath = "IdPlan";
        }

        public async Task ViewAllEmployee()
        {
            sale = await SaleFromDb.GetSale();

            dataGridSale.ItemsSource = sale;
        }

        List<Sale> SearchSale(string searchString)
        {
            saleSearch.Clear();

            foreach (Sale item in sale)
            {
                if (item.Fio.StartsWith(searchString))
                {
                    saleSearch.Add(item);
                }
            }

            return saleSearch;
        }


        private void txbSearchs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbSearchs.Text.Length != 0)
            {
                dataGridSale.ItemsSource = SearchSale(txbSearchs.Text);
            }
            else
            {
                dataGridSale.ItemsSource = sale;
            }
        }

        private async void comboBoxSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxSale.SelectedIndex == 0)
            {
                await ViewAllEmployee();
            }
            else
            {
                sale = await SaleFromDb.FilterSaleByPlan(comboBoxSale.SelectedIndex);

                dataGridSale.ItemsSource = sale;
            }

        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSale.SelectedItem != null)
            {
                Sale selectedEdit = (Sale)dataGridSale.SelectedItem;
                string warning = "Вы действительно хотите редактировать продажу от " + selectedEdit.DateOfSale + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    AddEditSaleWindow addEditSaleWindow = new AddEditSaleWindow();

                    CurrentSale = (Sale)dataGridSale.SelectedItem;

                    AddEditSaleWindow.selectedIndex = sale.FindIndex(u => u.IdSale == selectedEdit.IdSale);

                    addEditSaleWindow.ShowDialog();

                    dataGridSale.ItemsSource = null;
                    dataGridSale.ItemsSource = sale;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продажу для редактирования.");
            }
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSale.SelectedItem != null)
            {
                Sale selected = (Sale)dataGridSale.SelectedItem;
                string warning = "Вы действительно хотите удалить продажу от " + selected.DateOfSale + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    await SaleFromDb.DeleteSale(selected);

                    sale.Remove(selected);

                    dataGridSale.ItemsSource = null;
                    dataGridSale.ItemsSource = sale;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продажу для удаления.");
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditSaleWindow addEditSaleWindow = new AddEditSaleWindow();
            addEditSaleWindow.ParentPage = this;
            addEditSaleWindow.ShowDialog();
        }
    }
}
