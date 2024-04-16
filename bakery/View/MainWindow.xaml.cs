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

            var menuRecite = new List<SubItem>
            {
                new SubItem("Склад"),
                new SubItem("Остатки"),
                new SubItem("Реализация")
            };
            var item6 = new ItemMenu("Склад", menuRecite, PackIconKind.Home);

            var menuProduct = new List<SubItem>
            {
                new SubItem("Продукты")
            };
            var item1 = new ItemMenu("Продукция", menuProduct, PackIconKind.Schedule);

            var menuGuide = new List<SubItem>
            {
                new SubItem("Единицы"),
                new SubItem("Рецепты"),
                new SubItem("Компании"),
                new SubItem("Поставщики"),
                new SubItem("Продажи"),
                new SubItem("Компании")
            };
            var item2 = new ItemMenu("Справочники",  menuGuide, PackIconKind.FileReport);

            var menuIngredient = new List<SubItem>
            {
                new SubItem("Тип ингредиента"),
                new SubItem("Ингредиенты"),
                new SubItem("Расход ингредиентов")
            };
            var item3 = new ItemMenu("Ингардиаенты", menuIngredient, PackIconKind.ShoppingBasket);

            var menuiEmployee = new List<SubItem>
            {
                new SubItem("Сотрудники"),
                new SubItem("Занятости"),
                new SubItem("Должности")
            };
            var item4 = new ItemMenu("Сотрудники", menuiEmployee, PackIconKind.ScaleBalance);

            var menuiUser = new List<SubItem>
            {
                new SubItem("Сотрудники"),
                new SubItem("Занятости"),
                new SubItem("Должности")
            };
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
            ProfileEditWindow profileEditWindow = new ProfileEditWindow();
            profileEditWindow.Show();
        }

        private void ChangePassword_Click_Exit(object sender, RoutedEventArgs e)
        {
            PasswordChangeWindow passwordChange = new PasswordChangeWindow();
            bool? result = passwordChange.ShowDialog();

            if (result == true)
            {
                Application.Current.MainWindow.Show();
                Close();
            }
        }

        private void ChangeUser_Click_Exit(object sender, RoutedEventArgs e)
        {
            AuthorisationsWindow.CurrentUser = null;
            Application.Current.MainWindow.Show();
            Close();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
