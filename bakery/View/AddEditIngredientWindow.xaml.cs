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
    /// Логика взаимодействия для AddEditIngredientWindow.xaml
    /// </summary>
    public partial class AddEditIngredientWindow : Window
    {
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

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
