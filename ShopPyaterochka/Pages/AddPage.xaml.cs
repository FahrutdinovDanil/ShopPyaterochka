using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace ShopPyaterochka.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        Product productToAdd;
        public static ObservableCollection<Product> products { get; set; }
        public AddPage(Product product)
        {
            InitializeComponent();
            DataContext = productToAdd;
            productToAdd = product;
            cb_unit.ItemsSource = db_connection.connection.Unit.ToList();
        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void tb_name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0) && e.Text != "-")
            {
                e.Handled = true;Product new_product = new Product();
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            productToAdd.Name = tb_name.Text;
            productToAdd.Description = tb_description.Text;
            productToAdd.AddDate = DateTime.Now;
            var unit = cb_unit.SelectedItem as Unit;
            productToAdd.UnitId = unit.Id;
            db_connection.connection.Product.Add(productToAdd);
            db_connection.connection.SaveChanges();
            products = new ObservableCollection<Product>(db_connection.connection.Product.ToList());
            NavigationService.Navigate(new ListPage(ListPage.user));
        }

        private void btn_newphoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.jpg|*.jpg|*.png|*.png"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                productToAdd.Photo = File.ReadAllBytes(openFile.FileName);
                img_prod.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }
    }
}
