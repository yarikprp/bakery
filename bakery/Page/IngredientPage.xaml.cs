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
    /// Логика взаимодействия для IngredientPage.xaml
    /// </summary>
    public partial class IngredientPage : System.Windows.Controls.Page
    {
        List<Ingredients> ingredients = new List<Ingredients>();
        List<Ingredients> ingredientsSearch = new List<Ingredients>();
        public IngredientPage()
        {
            InitializeComponent();
        }

        private async void IngredientPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllIngredients();
        }

        async Task ViewAllIngredients()
        {
            ingredients = await IngredientsFromDb.GetIngredients();

            dataGridIngredient.ItemsSource = ingredients;
        }

        List<Ingredients> SearchUsers(string searchString)
        {
            ingredientsSearch.Clear();

            foreach (Ingredients item in ingredients)
            {
                if (item.NameIngredients.StartsWith(searchString) || item.Warehouse.StartsWith(searchString))
                {
                    ingredientsSearch.Add(item);
                }
            }

            return ingredientsSearch;
        }


        private void txbSearchs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbSearchs.Text.Length != 0)
            {
                dataGridIngredient.ItemsSource = SearchUsers(txbSearchs.Text);
            }
            else
            {
                dataGridIngredient.ItemsSource = ingredients;
            }
        }

        private void comboBoxIngredient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridIngredient.SelectedItem != null)
            {
                Ingredients selected = (Ingredients)dataGridIngredient.SelectedItem;
                string warning = "Вы действительно хотите удалить " + selected.NameIngredients  + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    await IngredientsFromDb.DeleteIngredients(selected);

                    ingredients.Remove(selected);

                    dataGridIngredient.ItemsSource = null;
                    dataGridIngredient.ItemsSource = ingredients;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите ингредиент для удаления.");
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
