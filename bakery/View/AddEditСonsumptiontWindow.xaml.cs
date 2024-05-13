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
    /// Логика взаимодействия для AddEditСonsumptiontWindow.xaml
    /// </summary>
    public partial class AddEditСonsumptiontWindow : Window
    {
        List<ConsumptionOfIngredients> consumptionOfIngredients = new List<ConsumptionOfIngredients>();
        public static int selectedIndex;
        public AddEditСonsumptiontWindow()
        {
            InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            consumptionOfIngredients = await ConsumptionFromDb.GetConsumptionOfIngredients();
            comboBoxPlan.ItemsSource = await ProductReleasePlanFromDb.GetProductReleasePlan();
            comboBoxPlan.DisplayMemberPath = "IdPlan";
            comboBoxPlan.SelectedValuePath = "IdPlan";
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
