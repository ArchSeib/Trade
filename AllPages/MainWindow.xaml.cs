using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Trade.AllPages;

namespace Trade.AllPages
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ImgLogotip.Source = new BitmapImage(new Uri("/Resources/logo.png", UriKind.RelativeOrAbsolute));
            Helper.BtnBack = this.BtnBack;
            Helper.BtnExitAccaunt = this.BtnExitAccaunt;
            Helper.TbFIO = this.TbFIO;
            Helper.MainFrame = this.MainFrame;
            Helper.MainFrame.Navigate(new AutрorizationPage());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnExitAccaunt_Click(object sender, RoutedEventArgs e)
        {
            Helper.MainFrame.Navigate(new AutрorizationPage());
            Helper.Role = "Гость";
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (Helper.levelPageActive <= 1)
            {
                Helper.BtnBack.Visibility = Visibility.Hidden;
            }
            else
            {
                Helper.BtnBack.Visibility = Visibility.Visible;
            }
            Helper.MainFrame.GoBack();
        }
    }
}
