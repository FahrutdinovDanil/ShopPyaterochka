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
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (CorrectPass())
            {
                int role = 3;
                var newUser = new User();
                var newClient = new Client();

                newUser.Login = tbLogin.Text;
                newUser.Password = tbPassword.Text;
                newUser.RoleId = role;
                db_connection.connection.User.Add(newUser);
                db_connection.connection.SaveChanges();

                users = new ObservableCollection<User>(db_connection.connection.User.ToList());

                newClient.FIO = tbFIO.Text;

                if (cbGender.Text == "Мужской")
                {
                    newClient.GenderId = 1;
                }
                else if (cbGender.Text == "Женский")
                {
                    newClient.GenderId = 2;
                }

                newClient.NumberPhone = tbPhone.Text;
                newClient.Email = tbEmail.Text;
                newClient.AddDate = DateTime.Now;
                newClient.UserId = users.Last().Id;

                db_connection.connection.Client.Add(newClient);
                db_connection.connection.SaveChanges();

                MessageBox.Show("All OK");
                NavigationService.GoBack();
            }
        }

        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        public bool CorrectPass()
        {
            users = new ObservableCollection<User>(db_connection.connection.User.ToList());
            bool login_unic = true;
            foreach (var i in users)
            {
                if (i.Login == tbLogin.Text)
                {
                    login_unic = false;
                }
            }

            if (login_unic)
            {
                string password = tbPassword.Text;
                bool letters = false;
                bool numbers = false;
                bool specialChar = false;
                foreach (var i in password)
                {
                    if (Char.IsLetter(i))
                    {
                        letters = true;
                    }
                }

                foreach (var i in password)
                {
                    if (Char.IsNumber(i))
                    {
                        numbers = true;
                    }
                }

                foreach (var i in password)
                {
                    if (i == '!' || i == '@' || i == '#' || i == '$' || i == '%' || i == '^')
                    {
                        specialChar = true;
                    }
                }

                if (password.Length > 6 && letters && numbers && specialChar)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Пароль должен содержать буквы, цифры и спец.символы");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Такой логин уже существует, придумайте новый");
                return false;
            }
        }
    }
}
