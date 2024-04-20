using bakery.Classes;
using bakery.Page;
using bakery.View;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FrameClass.userManegmentFrame = userManegmentFrame;
            FrameClass.userManegmentFrame.Navigate(new Page.UserManegmentFrame());
            FrameClass.warehouseFrame = warehouseFrame;
            FrameClass.warehouseFrame.Navigate(new Page.WarehouseFrame());
            FrameClass.companyFrame = companyFrame;
            FrameClass.companyFrame.Navigate(new Page.WarehouseFrame());
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileEditWindow profileEditWindow = new ProfileEditWindow();
            profileEditWindow.Show();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordChangeWindow passwordChange = new PasswordChangeWindow();
            bool? result = passwordChange.ShowDialog();

            if (result == true)
            {
                Application.Current.MainWindow.Show();
                Close();
            }
        }

        private void ChangeUser_Click(object sender, RoutedEventArgs e)
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
