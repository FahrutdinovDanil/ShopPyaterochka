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
    /// Логика взаимодействия для IntakePage.xaml
    /// </summary>
    public partial class IntakePage : Page
    {
        public static ObservableCollection<ProductIntakeProduct> intakeProducts { get; set; }
        public IntakePage(ProductIntake intake)
        {
            InitializeComponent();
            intakeProducts = new ObservableCollection<ProductIntakeProduct>((db_connection.connection.ProductIntakeProduct.Where(n => n.ProductIntake.Id == intake.Id)).ToList());

            cb_supplier.SelectedItem = intake.Supplier.Name;

            cb_supplier.ItemsSource = db_connection.connection.Supplier.ToList();
            cb_supplier.DisplayMemberPath = "Name";
            this.DataContext = this;
        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdmissionPage());
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

