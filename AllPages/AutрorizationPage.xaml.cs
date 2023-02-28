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
using Trade.AllPages;

namespace Trade.AllPages
{
    /// <summary>
    /// Логика взаимодействия для AutрorizationPage.xaml
    /// </summary>
    public partial class AutрorizationPage : Page
    {
        public AutрorizationPage()
        {
            InitializeComponent();
            PasswordChange = false;
            LoginChange = false;
        }

        private void BtnAutho_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder erros = new StringBuilder();
            if (TbxLogin.Text == "")
            {
                erros.AppendLine("Вы не ввели логин");
            }
            if (TbxPassword.Text == "")
            {
                erros.AppendLine("Вы не ввели пароль");
            }
            if (erros.Length != 0)
            {
                MessageBox.Show(erros.ToString());
            }
            else
            {
                try
                {
                    User user = Helper.GetData().User.Where(a => a.UserLogin == TbxLogin.Text).FirstOrDefault();
                    if (user == null)
                    {
                        MessageBox.Show("Такого пользователя не существует");
                    }
                    else
                    {
                        if (user.UserPassword != TbxPassword.Text)
                        {
                            MessageBox.Show("Неверно введён пароль");
                        }
                        else
                        {
                            if (user.UserRole == 3)
                            {
                                Helper.Role = "Менеджер";
                                Helper.MainFrame.Navigate(new ManagerPage());
                            }
                            if (user.UserRole == 2)
                            {
                                Helper.Role = "Админ";
                                Helper.MainFrame.Navigate(new AdminPage());
                            }
                            if (user.UserRole == 1)
                            {
                                Helper.Role = "Клиент";
                                Helper.MainFrame.Navigate(new ClientPage());
                            }
                            Helper.BtnExitAccaunt.Visibility = Visibility.Visible;
                            Helper.levelPageActive++;
                            Helper.TbFIO.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            Helper.MainFrame.Navigate(new ProductsPage());
        }
        private static bool LoginChange;
        private void TbxLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if(TbxLogin.Text== "Логин" && LoginChange == false)
            {
                TbxLogin.Text = "";
            }
        }

        private void TbxLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TbxLogin.Text == "")
            {
                TbxLogin.Text = "Логин";
                LoginChange = false;
            }
        }
        private void TbxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbxLogin.Text != "")
            {
                LoginChange = true;
            }
            if(TbxLogin.Text=="")
            {
                LoginChange = false;
            }
        }

        private static bool PasswordChange;
        private void TbxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TbxPassword.Text == "Пароль" && PasswordChange == false)
            {
                TbxPassword.Text = "";
            }
        }

        private void TbxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TbxPassword.Text == "")
            {
                TbxPassword.Text = "Пароль";
                PasswordChange = false;
            }
        }

        private void TbxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
                if (TbxPassword.Text != "")
                {
                    PasswordChange = true;
                }
                if (TbxPassword.Text == "")
                {
                    PasswordChange = false;
                }
        }
    }
}
