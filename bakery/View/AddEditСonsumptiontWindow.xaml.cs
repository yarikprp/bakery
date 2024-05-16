using bakery.Classes;
using bakery.Model;
using bakery.Page;
using System;
using System.Collections.Generic;
using System.Windows;

namespace bakery.View
{
    /// <summary>
    /// Логика взаимодействия для AddEditСonsumptiontWindow.xaml
    /// </summary>
    public partial class AddEditСonsumptiontWindow : Window
    {
        public СonsumptionOfIngredientsPage ParentPage { get; set; }

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

        private async void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(ValidateInput())
            {
                int consumption = Convert.ToInt32(textBoxСonsumption.Text);
                int plan = Convert.ToInt32(comboBoxPlan.Text);
                await ConsumptionFromDb.AddConsumption(plan, consumption);
                Close();
                if (ParentPage != null)
                {
                    await ParentPage.ViewAllConsumptionOfIngredients();
                }

            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBoxСonsumption.Text))
            {
                MessageBox.Show("Поле 'Расход' не может быть пустым.");
                return false;
            }

            int consumption;
            if (!int.TryParse(textBoxСonsumption.Text, out consumption))
            {
                MessageBox.Show("Поле 'Количество' должно содержать только числовое значение.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBoxPlan.Text))
            {
                MessageBox.Show("Поле 'План' не может быть пустым.");
                return false;
            }

            return true;
        }
    }
}
