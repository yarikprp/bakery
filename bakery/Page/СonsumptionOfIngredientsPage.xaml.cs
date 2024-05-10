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
    /// Логика взаимодействия для СonsumptionOfIngredientsPage.xaml
    /// </summary>
    public partial class СonsumptionOfIngredientsPage : System.Windows.Controls.Page
    {
        List<ConsumptionOfIngredients> consumptionOfIngredients = new List<ConsumptionOfIngredients>();
        List<ConsumptionOfIngredients> consumptionOfIngredientsSearch = new List<ConsumptionOfIngredients>();
        public СonsumptionOfIngredientsPage()
        {
            InitializeComponent();
        }

        private async void ConsumptionOfIngredients_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllConsumptionOfIngredients();
        }

        async Task ViewAllConsumptionOfIngredients()
        {
            consumptionOfIngredients = await ConsumptionFromDb.GetConsumptionOfIngredients();

            dataGridСonsumptionOfIngredientsPage.ItemsSource = consumptionOfIngredients;
        }

        List<ConsumptionOfIngredients> SearchEmployee(string searchString)
        {
            consumptionOfIngredientsSearch.Clear();

            foreach (ConsumptionOfIngredients item in consumptionOfIngredients)
            {
                /*if (item.Fio.StartsWith(searchString))
                {
                    consumptionOfIngredientsSearch.Add(item);
                }*/
            }

            return consumptionOfIngredientsSearch;
        }


        private void txbSearchs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbSearchs.Text.Length != 0)
            {
                dataGridСonsumptionOfIngredientsPage.ItemsSource = SearchEmployee(txbSearchs.Text);
            }
            else
            {
                dataGridСonsumptionOfIngredientsPage.ItemsSource = consumptionOfIngredients;
            }
        }

        private void comboBoxСonsumptionOfIngredientsPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
