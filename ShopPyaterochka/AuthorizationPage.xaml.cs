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

namespace ShopPyaterochka
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static ObservableCollection<User> users { get; set; }
        public AuthorizationPage()
        {
            InitializeComponent();
        }
        private void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void btn_authorization_Click(object sender, RoutedEventArgs e)
        {
            users = new ObservableCollection<User>(db_connection.connection.User.ToList());
            var z = users.Where(a => a.Login == tb_login.Text && a.Password == tb_password.Text).FirstOrDefault();
            if (z != null)
            {
                NavigationService.Navigate(new ListPage());
            }
            else
            {
                MessageBox.Show("Логин или пароль неверный", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
