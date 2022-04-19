using System;
using System.IO;
using Microsoft.Win32;
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

namespace ShopPyaterochka.Pages
{
    /// <summary>
    /// Логика взаимодействия для RedactionPage.xaml
    /// </summary>
    public partial class RedactionPage : Page
    {
        public static Product constProd;
        public RedactionPage(Product n)
        {
            InitializeComponent();
            constProd = n;
            this.DataContext = constProd;
            tb_id.Text = n.Id.ToString();
            tb_name.Text = n.Name;
            tb_description.Text = n.Description;
            cb_unit.ItemsSource = db_connection.connection.Unit.ToList();
            cb_country.ItemsSource = db_connection.connection.Country.ToList();
            cb_country.DisplayMemberPath = "Name";
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListPage(ListPage.user));
        }

        public static bool DeleteProduct(Product product)
        {
            product.IsDeleted = true;
            try
            {
                db_connection.connection.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btn_delite_Click(object sender, RoutedEventArgs e)
        {
            DeleteProduct(constProd);
            MessageBox.Show($"Продукт {constProd.Name} удалён");
            NavigationService.Navigate(new ListPage(ListPage.user));
        }

        private void tb_name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0) && e.Text != "-")
            {
                e.Handled = true;
            }
        }

        private void btn_newphoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.jpg|*.jpg|*.png|*.png"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                constProd.Photo = File.ReadAllBytes(openFile.FileName);
                img_prod.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            constProd.Name = tb_name.Text;
            constProd.Description = tb_description.Text;
            constProd.AddDate = DateTime.Now;
            var unit = cb_unit.SelectedItem as Unit;
            constProd.UnitId = unit.Id;
            db_connection.connection.SaveChanges();
            NavigationService.Navigate(new ListPage(ListPage.user));
        }

        private void btn_add_country_Click(object sender, RoutedEventArgs e)
        {
            if (cb_country.SelectedIndex >= 0)
            {
                var countryProd = new ProductCountry();
                var selectCountry = cb_country.SelectedItem as Country;
                countryProd.ProductId = constProd.Id;
                countryProd.CountryId = selectCountry.Id;
                var isCountry = db_connection.connection.ProductCountry.Where(a => a.CountryId == selectCountry.Id && a.ProductId == constProd.Id).Count();
                if (isCountry == 0)
                {
                    db_connection.connection.ProductCountry.Add(countryProd);
                    db_connection.connection.SaveChanges();
                    UpdateCountry();
                }
            }
        }

        public void UpdateCountry()
        {
            lv_country.ItemsSource = db_connection.connection.ProductCountry.Where(a => a.ProductId == constProd.Id).ToList();
        }

        private void btn_del_country_Click(object sender, RoutedEventArgs e)
        {
            if (lv_country.SelectedItem != null)
            {
                var selectProdCountry = db_connection.connection.ProductCountry.ToList().Find(a => a.ProductId == constProd.Id && a.CountryId == (lv_country.SelectedItem as ProductCountry).CountryId);
                db_connection.connection.ProductCountry.Remove(selectProdCountry);
                db_connection.connection.SaveChanges();
                UpdateCountry();
            }
        }
    }
}
