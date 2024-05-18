using bakery.Model;
using bakery.Page;
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
using System.Windows.Shapes;

namespace bakery.View
{
    /// <summary>
    /// Логика взаимодействия для ProfileEditWindow.xaml
    /// </summary>
    public partial class ProfileEditWindow : Window
    {
        public UserManegmentPage ParentPage { get; set; }

        public ProfileEditWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, EventArgs e)
        {
            textBoxFirstName.Text = AuthorisationsWindow.CurrentUser.FirstName;
            textBoxLastName.Text = AuthorisationsWindow.CurrentUser.LastName;
            textBoxPatronymic.Text = AuthorisationsWindow.CurrentUser.Patronymic;
            dateTimePickerBirthDay.SelectedDate = AuthorisationsWindow.CurrentUser.DateOfBirthday;
            textBoxPhone.Text = AuthorisationsWindow.CurrentUser.Phone;
            textBoxAdress.Text = AuthorisationsWindow.CurrentUser.Adress;
            textBoxEmail.Text = AuthorisationsWindow.CurrentUser.Email;
        }

        private async void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxFirstName.Text != "" && textBoxLastName.Text != "")
            {
                AuthorisationsWindow.CurrentUser.FirstName = textBoxFirstName.Text;
                AuthorisationsWindow.CurrentUser.LastName = textBoxLastName.Text;
                AuthorisationsWindow.CurrentUser.Patronymic = textBoxPatronymic.Text;
                AuthorisationsWindow.CurrentUser.DateOfBirthday = dateTimePickerBirthDay.SelectedDate.HasValue ? dateTimePickerBirthDay.SelectedDate.Value : default(DateTime);
                AuthorisationsWindow.CurrentUser.Phone = textBoxPhone.Text;
                AuthorisationsWindow.CurrentUser.Adress = textBoxAdress.Text;
                AuthorisationsWindow.CurrentUser.Email = textBoxEmail.Text;

                await UserFromDb.UpdateUserProfile(AuthorisationsWindow.CurrentUser);

                Close();
            }
            else
            {
                MessageBox.Show("Имя и фамилия являются обязательными полями для заполнения.");
            }
            if (ParentPage != null)
            {
                await ParentPage.ViewAllUsers();
            }
        }
    }
}
