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
        private static List<Product> productItems;
        private static bool SearchChange;
        private static Int32 TagFilter;
        private static Int32 TagSort;
        private List<Product> selectProducts = new List<Product>();
        private List<Product> NewOrder = new List<Product>();
        public ProductsPage()
        {
            InitializeComponent();
            BtnNewOrder.Visibility = Visibility.Visible;
            if (Helper.Role == "Админ")
            {
                BtnNewProduct.Visibility = Visibility.Visible;
                CMAdminMenu.Visibility = Visibility.Visible;
            }
            BtnNewOrder.IsEnabled = false;
            Load();
        }
        private static void CreateProductList()
        {
            productItems = new List<Product>();
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
            TbAllRecor.Text = productItems.Count.ToString();
            if (LVProducts != null)
            {
                if(TagSort!=0)
                    Sort();
                if(TagFilter!=0)
                    Filter();
                Search();
                LVProducts.ItemsSource = productItems;
            }
            TbVisibleRecor.Text = productItems.Count.ToString();
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
            if (NewOrder.Count != 0)
            {
                NewOrderWindow newOrderWindow = new NewOrderWindow(NewOrder);
                newOrderWindow.Show();
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (selectProducts.Count > 1)
            {
                MessageBox.Show("Вы должны выбрать только один элемент для редактирования");
            }
            else
            {
                NewProductWindow newProduct = new NewProductWindow(selectProducts[0]);
                newProduct.Show();
            }
        }

        private void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (selectProducts.Count == 0)
            {
                MessageBox.Show("Выбирите элементы для удаления");
            }
            else
            {
                var message = MessageBox.Show("Вы уверены что хотите удалить данный(ые) товар(ы)?", "Удаление товара!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (message == MessageBoxResult.Yes)
                {
                    try
                    {
                        foreach(Product productDelete in selectProducts)
                        {
                            Helper.GetData().Product.Remove(productDelete);
                        }
                        Helper.GetData().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void LVProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*            MessageBox.Show((LVProducts.SelectedItem as Product).ProductName);*/
            selectProducts = LVProducts.SelectedItems.Cast<Product>().ToList();
        }

        private void CMAddinOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectProducts.Count >= 1)
            {
                foreach (Product product in selectProducts)
                {
                    NewOrder.Add(product);
                }
                BtnNewOrder.Visibility = Visibility.Visible;
            }
            if (selectProducts.Count == 0)
            {
                MessageBox.Show("Выбирите хотябы один товр для добавления к заказу");
                BtnNewOrder.Visibility = Visibility.Hidden;
            }
        }

        private void BtnNewProduct_Click(object sender, RoutedEventArgs e)
        {
            NewProductWindow newProductWindow = new NewProductWindow(null);
            newProductWindow.Show();
        }
    }
}
