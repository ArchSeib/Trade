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
    /// Логика взаимодействия для SecondMenuPage.xaml
    /// </summary>
    public partial class SecondMenuPage : Page
    {
        public SecondMenuPage()
        {
            InitializeComponent();
        }
        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            Helper.MainFrame.Navigate(new ProductsPage());
            Helper.levelPageActive++;
            Helper.BtnBack.Visibility = Visibility.Visible;
        }
        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            Helper.MainFrame.Navigate(new OrdersPage());
            Helper.levelPageActive++;
            Helper.BtnBack.Visibility = Visibility.Visible;
        }
    }
}
