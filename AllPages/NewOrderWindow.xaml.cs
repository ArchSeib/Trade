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
using System.Windows.Shapes;
using Trade.AllClass;

namespace Trade.AllPages
{
    /// <summary>
    /// Логика взаимодействия для NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        List<Product> _orders = new List<Product>();
        private static bool searhProduct = true;
        public NewOrderWindow(List<Product> orders)
        {
            InitializeComponent();

            foreach (Product product in orders)
            {
                if (_orders.Count != 0)
                {
                    foreach (Product itemInOrder in _orders)
                    {
                        if (itemInOrder.ProductName == product.ProductName)
                        {
                            itemInOrder.CountInOrder++;
                            searhProduct = false;
                            break;
                        }
                    }
                    if (searhProduct)
                    {
                        _orders.Add(product);
                    }
                    searhProduct = true;
                }
                else
                {
                    _orders.Add(product);
                    _orders.Last().CountInOrder = 1;
                }
            }
            TblDate.Text = DateTime.Now.ToShortDateString();
            TblNumber.Text = (Helper.GetData().Order.ToList().Count + 1).ToString();
            Random rnd = new Random();
            String Code = "";
            for (Int32 i = 0; i < 3; i++)
            {
                Code += rnd.Next(0, 9);
            }
            TblCode.Text = Code;
            CBAddres.ItemsSource = Helper.GetData().PickupPoint.ToList();
            Load();
        }
        private void Load()
        {
            LVProducts.ItemsSource = _orders;
            Decimal cost = 0;
            Decimal Discount = 0;
            foreach (Product product in _orders)
            {
                if (product.ProductDiscountAmount != 0)
                {
                    Discount += product.ProductCost * Convert.ToDecimal(product.ProductDiscountAmount) / 100 * product.CountInOrder;
                }
                cost += product.ProductCost * product.CountInOrder;
            }
            TblCos.Text = cost.ToString();
            TblCostDicount.Text = Discount.ToString();

        }
        public static Boolean IsNumeric(string stringToTest)
        {
            int result;
            return int.TryParse(stringToTest, out result);
        }
        private void TBCountProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsNumeric((sender as TextBox).Text))
            {
                if(Convert.ToInt32((sender as TextBox).Text) == 0){
                    _orders.Remove(LVProducts.SelectedItem as Product);
                }
                else
                {
                    Product product = LVProducts.SelectedItem as Product;
                    foreach (Product item in _orders)
                    {
                        if (item.ProductName == product.ProductName)
                        {
                            item.CountInOrder = Convert.ToInt32((sender as TextBox).Text);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Вводить можно только числа!");
            }
            Load();
        }

        private void BtnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            order.OrderID = Convert.ToInt32(TblNumber.Text);
            order.OrderStatus = "Новый";
            order.OrderDate = DateTime.Now;
            order.OrderPickupPoint = (CBAddres.SelectedItem as PickupPoint).IDPickupPoint;
            if (Helper.Role != "Гость")
            {
                order.ClientFullName = Helper.TbFIO.Text;
            }
            order.ReceptionCode = Convert.ToInt32(TblCode.Text);
            DateTime dateNow = DateTime.Now;
            foreach (Product item in _orders)
            {
                if(item.ProductQuantityInStock < item.CountInOrder)
                {
                    dateNow.AddDays(3);
                    break;
                }
            }
            order.OrderDeliveryDate = dateNow.AddDays(3);
            try
            {
                Helper.GetData().Order.Add(order);
                Helper.GetData().SaveChanges();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Close();
            }
        }
    }
}
