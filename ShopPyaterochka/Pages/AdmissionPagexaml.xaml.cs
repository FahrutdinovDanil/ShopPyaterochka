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

namespace ShopPyaterochka.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdmissionPagexaml.xaml
    /// </summary>
    public partial class AdmissionPage : Page
    {
        public static ObservableCollection<ProductIntake> intakes { get; set; }
        public AdmissionPage()
        {
            InitializeComponent();
            intakes = new ObservableCollection<ProductIntake>(db_connection.connection.ProductIntake.ToList());
            this.DataContext = this;
        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListPage(ListPage.user));
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lv_postup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var n = (sender as ListView).SelectedItem as ProductIntake;

            NavigationService.Navigate(new IntakePage(n));
        }
    }
}
