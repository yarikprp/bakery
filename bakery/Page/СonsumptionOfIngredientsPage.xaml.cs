using bakery.Classes;
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
    public partial class СonsumptionOfIngredientsPage : System.Windows.Controls.Page
    {
        List<ConsumptionOfIngredients> consumptionOfIngredients = new List<ConsumptionOfIngredients>();
        List<ConsumptionOfIngredients> consumptionOfIngredientsSearch = new List<ConsumptionOfIngredients>();
        public static ConsumptionOfIngredients CurrentConsumption { get; set; } = null;
        public СonsumptionOfIngredientsPage()
        {
            InitializeComponent();
        }

        private async void ConsumptionOfIngredients_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllConsumptionOfIngredients();
            consumptionOfIngredients = await ConsumptionFromDb.GetConsumptionOfIngredients();
            consumptionOfIngredients.Insert(0, new ConsumptionOfIngredients(0, 0, 0));

            comboBoxСonsumptionOfIngredientsPage.ItemsSource = consumptionOfIngredients;
            comboBoxСonsumptionOfIngredientsPage.DisplayMemberPath = "IdConsumption";
            comboBoxСonsumptionOfIngredientsPage.SelectedValuePath = "IdConsumption";
        }

        public async Task ViewAllConsumptionOfIngredients()
        {
            consumptionOfIngredients = await ConsumptionFromDb.GetConsumptionOfIngredients();

            dataGridСonsumptionOfIngredientsPage.ItemsSource = consumptionOfIngredients;
        }

        List<ConsumptionOfIngredients> SearchEmployee(string searchString)
        {
            consumptionOfIngredientsSearch.Clear();

            foreach (ConsumptionOfIngredients item in consumptionOfIngredients)
            {
                if (item.IdPlan.ToString().StartsWith(searchString))
                {
                    consumptionOfIngredientsSearch.Add(item);
                }
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

        private async void comboBoxСonsumptionOfIngredientsPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxСonsumptionOfIngredientsPage.SelectedIndex == 0)
            {
                await ViewAllConsumptionOfIngredients();
            }
            else
            {
                consumptionOfIngredients = await ConsumptionFromDb.FilterConsumptionOfIngredientsById(comboBoxСonsumptionOfIngredientsPage.SelectedIndex);

                dataGridСonsumptionOfIngredientsPage.ItemsSource = consumptionOfIngredients;
            }

        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridСonsumptionOfIngredientsPage.SelectedItem != null)
            {
                ConsumptionOfIngredients selectedEdit = (ConsumptionOfIngredients)dataGridСonsumptionOfIngredientsPage.SelectedItem;
                string warning = "Вы действительно хотите редактировать расход?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    AddEditСonsumptiontWindow addEditСonsumptiontWindow = new AddEditСonsumptiontWindow();

                    CurrentConsumption = (ConsumptionOfIngredients)dataGridСonsumptionOfIngredientsPage.SelectedItem;

                    addEditСonsumptiontWindow.ShowDialog();

                    dataGridСonsumptionOfIngredientsPage.ItemsSource = null;
                    dataGridСonsumptionOfIngredientsPage.ItemsSource = consumptionOfIngredients;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите расход для редактирования.");
            }

        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridСonsumptionOfIngredientsPage.SelectedItem != null)
            {
                ConsumptionOfIngredients selected = (ConsumptionOfIngredients)dataGridСonsumptionOfIngredientsPage.SelectedItem;
                string warning = "Вы действительно хотите удалить расход ?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    await ConsumptionFromDb.DeleteConsumptionOfIngredients(selected);

                    consumptionOfIngredients.Remove(selected);

                    dataGridСonsumptionOfIngredientsPage.ItemsSource = null;
                    dataGridСonsumptionOfIngredientsPage.ItemsSource = consumptionOfIngredients;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите расход для удаления.");
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditСonsumptiontWindow addEditСonsumptiontWindow = new AddEditСonsumptiontWindow();
            addEditСonsumptiontWindow.ParentPage = this;
            addEditСonsumptiontWindow.ShowDialog();
        }
    }
}
