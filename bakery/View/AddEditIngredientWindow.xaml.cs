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
    /// <summary>
    /// Логика взаимодействия для AddEditIngredientWindow.xaml
    /// </summary>
    public partial class AddEditIngredientWindow : Window
    {
        public IngredientPage ParentPage { get; set; }
        List<Ingredients> ingredients = new List<Ingredients>();
        public static int selectedIndex;
        public AddEditIngredientWindow()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ingredients = await IngredientsFromDb.GetIngredients();

            comboBoxType.ItemsSource = await TypeIngredientsFromDb.GetTypeIngredients();
            comboBoxType.DisplayMemberPath = "TypeIngredient";
            comboBoxType.SelectedValuePath = "IdType";


            comboBoxProduct.ItemsSource = await ProductFromDb.GetProduct();
            comboBoxProduct.DisplayMemberPath = "NameProduct";
            comboBoxProduct.SelectedValuePath = "IdProduct";


            comboBoxUnit.ItemsSource = await UnitFromDb.GetUnit();
            comboBoxUnit.DisplayMemberPath = "NameUnit";
            comboBoxUnit.SelectedValuePath = "IdUnit";

        }

        private async void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInput())
            {
                string ingredient = textBoxFIngredient.Text;
                string type = comboBoxType.Text;
                string product = comboBoxProduct.Text;
                string unit = comboBoxUnit.Text;
                int quantity = Convert.ToInt32(textBoxQuantity.Text);
                string warehouse = textBoxWarehouse.Text;
                await IngredientsFromDb.AddIngredients(ingredient, type, product, unit, quantity, warehouse);
                Close();
                if (ParentPage != null)
                {
                    await ParentPage.ViewAllIngredients();
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBoxFIngredient.Text))
            {
                MessageBox.Show("Поле 'Ингредиент' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBoxType.Text))
            {
                MessageBox.Show("Поле 'Тип ингредиента' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBoxProduct.Text))
            {
                MessageBox.Show("Поле 'Продукт' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBoxUnit.Text))
            {
                MessageBox.Show("Поле 'Единица' не может быть пустым.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxWarehouse.Text))
            {
                MessageBox.Show("Поле 'Склад' не может быть пустым.");
                return false;
            }

            return true;
        }
    }
}
