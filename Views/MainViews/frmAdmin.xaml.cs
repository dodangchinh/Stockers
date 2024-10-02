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
using System.Collections.ObjectModel;

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for frmAdmin.xaml
    /// </summary>
    public partial class frmAdmin : Window
    {
        Account accountLogin;
        Button tempBtn = new Button();

        public frmAdmin(Account accountLogin)
        {
            InitializeComponent();
            txtNameAccount.Text += accountLogin.Name;

            this.accountLogin = accountLogin;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1280;
                    this.Height = 780;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximize = true;
                }
            }
        }
        private void CheckClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;             

            button.Background = Brushes.White;
            button.Foreground = Brushes.OrangeRed;

            foreach (Button button1 in Helper.FindVisualChildren<Button>(spBtnMenu))
            {
                if (button1.Background == Brushes.White && button1 != button)
                {
                    button1.Background = tempBtn.Background;
                    button1.Foreground = tempBtn.Foreground;
                }

            }
        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            spMain.Children.Clear();
            UcDisplayAccount ucDisplayAccount = new UcDisplayAccount(accountLogin);
            spMain.Children.Add(ucDisplayAccount);
        }

        private void btnRole_Click(object sender, RoutedEventArgs e)
        {
            spMain.Children.Clear();
            UcDisplayRole ucDisplayRole = new UcDisplayRole(accountLogin);
            spMain.Children.Add(ucDisplayRole);
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            spMain.Children.Clear();
            UcSelectedProduct ucSelectedProduct = new UcSelectedProduct(accountLogin, true);
            spMain.Children.Add(ucSelectedProduct);
        }
    }
}
