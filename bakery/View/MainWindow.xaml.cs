using bakery.View;
using bakery.ViewModel;
using MaterialDesignThemes.Wpf;
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

namespace bakery
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var menuRecite = new List<SubItem>();
            menuRecite.Add(new SubItem("Склад"));
            menuRecite.Add(new SubItem("Остатки"));
            menuRecite.Add(new SubItem("Реализация")); 
            var item6 = new ItemMenu("Склад", menuRecite, PackIconKind.Home);

            var menuProduct = new List<SubItem>();
            menuProduct.Add(new SubItem("Продукты"));
            var item1 = new ItemMenu("Продукция", menuProduct, PackIconKind.Schedule);

            var menuGuide = new List<SubItem>();
            menuGuide.Add(new SubItem("Единицы"));
            menuGuide.Add(new SubItem("Рецепты"));
            menuGuide.Add(new SubItem("Компании")); 
            menuGuide.Add(new SubItem("Поставщики")); 
            menuGuide.Add(new SubItem("Продажи"));  
            menuGuide.Add(new SubItem("Компании")); 
            var item2 = new ItemMenu("Справочники",  menuGuide, PackIconKind.FileReport);

            var menuIngredient = new List<SubItem>();
            menuIngredient.Add(new SubItem("Тип ингредиента"));
            menuIngredient.Add(new SubItem("Ингредиенты"));
            menuIngredient.Add(new SubItem("Расход ингредиентов")); 
            var item3 = new ItemMenu("Ингардиаенты", menuIngredient, PackIconKind.ShoppingBasket);

            var menuiEmployee = new List<SubItem>();
            menuiEmployee.Add(new SubItem("Сотрудники"));
            menuiEmployee.Add(new SubItem("Занятости"));
            menuiEmployee.Add(new SubItem("Должности")); 
            var item4 = new ItemMenu("Сотрудники", menuiEmployee, PackIconKind.ScaleBalance);

            var menuiUser = new List<SubItem>();
            menuiUser.Add(new SubItem("Сотрудники"));
            menuiUser.Add(new SubItem("Занятости"));
            menuiUser.Add(new SubItem("Должности")); 
            var item0 = new ItemMenu("Пользователи", menuiUser, PackIconKind.Register);

            Menu.Children.Add(new UserControl1(item0));
            Menu.Children.Add(new UserControl1(item6));
            Menu.Children.Add(new UserControl1(item1));
            Menu.Children.Add(new UserControl1(item2));
            Menu.Children.Add(new UserControl1(item3));
            Menu.Children.Add(new UserControl1(item4));
        }

        private void EditProfile_Click_Exit(object sender, RoutedEventArgs e)
        {

        }

        private void ChangePassword_Click_Exit(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeUser_Click_Exit(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
