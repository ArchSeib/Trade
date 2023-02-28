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
                    item.ColorCodeDiscount = 1;
                }
                else
                {
                    item.ColorCodeDiscount = 0;
                }
                item.ProductRealCost = item.ProductCost- item.ProductCost*Convert.ToDecimal(item.ProductDiscountAmount);
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
            LVProducts.ItemsSource = productItems;
        }
        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TbSeacrh_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnNewOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
