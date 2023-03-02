using System;
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
using Trade.AllClass;

namespace Trade.AllPages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        private static List<Product> productItems = new List<Product>();
        private static bool SearchChange;
        private static Int32 TagFilter;
        private static Int32 TagSort;
        public ProductsPage()
        {
            InitializeComponent();
            if (Helper.Role == "Клиент")
            {
                BtnNewOrder.Visibility = Visibility.Visible;
            }
            if (Helper.Role == "Админ")
            {
                BtnDeleteProduct.Visibility = Visibility.Visible;
                BtnEditProduct.Visibility = Visibility.Visible;
                BtnNewOrder.Visibility = Visibility.Visible;
                BtnNewProduct.Visibility = Visibility.Visible;
            }
            Load();
        }
        private static void CreateProductList()
        {
            foreach(Product item in Helper.GetData().Product.ToList())
            {
                if (item.ProductDiscountAmount >= 15)
                {
                    item.ColorCodeDiscount = "#7fff00";
                }
                else
                {
                    item.ColorCodeDiscount = "#FF76E383";
                }
                item.ProductRealCost = item.ProductCost - item.ProductCost*Convert.ToDecimal(item.ProductDiscountAmount)/100;
                if (item.ProductPhoto == null)
                {
                    item.PathPhoto = new BitmapImage(new Uri("/Resources/picture.png", UriKind.Relative));
                }
                else
                {
                    item.PathPhoto = new BitmapImage(new Uri(item.ProductPhoto, UriKind.Relative));
                }
                productItems.Add(item);
            }
        }
        private void Load()
        {
            CreateProductList();
            if (LVProducts != null)
            {
                if(TagSort!=0)
                    Sort();
                if(TagFilter!=0)
                    Filter();
                Search();
                LVProducts.ItemsSource = productItems;
            }
        }
        private void Sort()
        {
            switch(TagSort)
            {
                case 1:
                    productItems = productItems.OrderBy(a => a.ProductCost).ToList();
                    break;
                case 2:
                    productItems = productItems.OrderByDescending(a => a.ProductCost).ToList();
                    break;
                default:
                    Load();
                    break;
            }
        }
        private void Filter()
        {
            switch (TagFilter)
            {
                case 1:
                    productItems = productItems.Where(a=>a.ProductDiscountAmount<9.99).ToList();
                    break;
                case 2:
                    productItems = productItems.Where(a => a.ProductDiscountAmount > 10 && a.ProductDiscountAmount<14.99).ToList();
                    break;
                case 3:
                    productItems = productItems.Where(a => a.ProductDiscountAmount > 15).ToList();
                    break;
                default:
                    Load();
                    break;
            }
        }
        private void Search()
        {
            productItems = productItems.Where(a=>a.ProductName.ToUpper().StartsWith(TbSeacrh.Text.ToUpper())).ToList();
        }
        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TagSort = Convert.ToInt32(((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag.ToString());
            Load();
        }

        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TagFilter = Convert.ToInt32(((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag.ToString());
            Load();
        }

        private void TbSeacrh_TextChanged(object sender, TextChangedEventArgs e)
        {
             SearchChange = true;
             Load();
        }

        private void BtnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            NewProductWindow newProductWindow = new NewProductWindow(null);
            newProductWindow.Show();
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            NewProductWindow newProductWindow = new NewProductWindow(null);
            newProductWindow.Show();
        }

        private void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Product deleteProduct = new Product();
            var message = MessageBox.Show("Вы уверены что хотите удалить данныё товар?","Удаление товара!",MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message == MessageBoxResult.Yes)
            {
                try
                {
                    Helper.GetData().Product.Remove(deleteProduct);
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
