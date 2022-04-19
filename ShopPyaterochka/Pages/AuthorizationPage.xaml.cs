using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ShopPyaterochka.BD;

namespace ShopPyaterochka
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static int attempts_count = 0;
        public static ObservableCollection<User> users { get; set; }
        public AuthorizationPage()
        {
            InitializeComponent();
            tbLogin.Text = Properties.Settings.Default.Login;
        }
        private void btn_registration_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void btn_authorization_Click(object sender, RoutedEventArgs e)
        {
            users = new ObservableCollection<User>(db_connection.connection.User.ToList());
            var z = users.Where(a => a.Login == tbLogin.Text && a.Password == tbPassword.Text).FirstOrDefault();
            if (z != null)
            {
                if (cbRemember.IsChecked.GetValueOrDefault())
                {
                    Properties.Settings.Default.Login = tbLogin.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.Login = null;
                    Properties.Settings.Default.Save();
                }
                NavigationService.Navigate(new ListPage(z));
            }
            else
            {
                attempts_count++;
                MessageBox.Show("Неверный логин или пароль", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (attempts_count == 3)
                {
                    attempts_count = 0;
                    Properties.Settings.Default.Password = DateTime.Now.AddMinutes(1);
                    Properties.Settings.Default.Save();
                    btnAuthorization.IsEnabled = false;
                }
            }
        }
    }
}
