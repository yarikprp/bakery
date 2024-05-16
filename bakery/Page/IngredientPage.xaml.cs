using bakery.Classes;
using bakery.Model;
using bakery.View;
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
        public static Ingredients CurrentIngredients { get; set; } = null;

        public IngredientPage()
        {
            InitializeComponent();
        }

        private async void IngredientPage_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewAllIngredients();
        }

        public async Task ViewAllIngredients()
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
            if (dataGridIngredient.SelectedItem != null)
            {
                Ingredients selectedEdit = (Ingredients)dataGridIngredient.SelectedItem;
                string warning = "Вы действительно редактировать " + selectedEdit.NameIngredients + "?";

                MessageBoxResult result = MessageBox.Show(warning, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    AddEditIngredientWindow addEditIngredientWindow = new AddEditIngredientWindow();

                    addEditIngredientWindow.textBoxFIngredient.Text = selectedEdit.NameIngredients;
                    addEditIngredientWindow.comboBoxType.Text = selectedEdit.TypeIngredients;
                    addEditIngredientWindow.comboBoxProduct.Text = selectedEdit.NameProduct;
                    addEditIngredientWindow.comboBoxUnit.Text = selectedEdit.NameUnit;
                    addEditIngredientWindow.textBoxQuantity.Text = selectedEdit.Quantity.ToString();
                    addEditIngredientWindow.textBoxWarehouse.Text = selectedEdit.Warehouse;

                    AddEditIngredientWindow.selectedIndex = ingredients.FindIndex(u => u.IdIngredients == selectedEdit.IdIngredients);

                    addEditIngredientWindow.ShowDialog();

                    dataGridIngredient.ItemsSource = null;
                    dataGridIngredient.ItemsSource = ingredients;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите ингредиент для редактирования.");
            }
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
            AddEditIngredientWindow addEditIngredientWindow = new AddEditIngredientWindow();
            addEditIngredientWindow.ParentPage = this;
            addEditIngredientWindow.ShowDialog();
        }
    }
}
