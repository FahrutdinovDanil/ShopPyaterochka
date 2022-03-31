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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {

        public static ObservableCollection<User> users { get; set; }
        public RegistrationPage()
        {
            InitializeComponent();
        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            int role = 3;
            var new_user = new User();
            var new_client = new Client();

            new_user.Login = tb_login.Text;
            new_user.Password = tb_password.Text;
            new_user.RoleId = role;
            db_connection.connection.User.Add(new_user);
            db_connection.connection.SaveChanges();

            users = new ObservableCollection<User>(db_connection.connection.User.ToList());

            new_client.FIO = tb_FIO.Text;

            if (cb_gender.Text == "Мужской")
            {
                new_client.GenderId = 1;
            }
            else if (cb_gender.Text == "Женский")
            {
                new_client.GenderId = 2;
            }

            new_client.NumberPhone = tb_phone.Text;
            new_client.Email = tb_email.Text;
            new_client.AddDate = DateTime.Now;
            new_client.UserId = users.Last().Id;

            db_connection.connection.Client.Add(new_client);
            db_connection.connection.SaveChanges();

            MessageBox.Show("All OK");
            NavigationService.GoBack();
        }
    }
}
