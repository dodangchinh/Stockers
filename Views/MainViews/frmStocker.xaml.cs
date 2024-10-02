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
    public partial class frmStocker : Window
    {
        Account accountLogin;
        Button tempBtn = new Button();

        public frmStocker(Account accountLogin)
        {
            InitializeComponent();

            this.accountLogin = accountLogin;
            txtNameAccount.Text += accountLogin.Name;
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

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            spMain.Children.Clear();
            UcSelectedProduct ucSelectedProduct = new UcSelectedProduct(accountLogin, false);
            spMain.Children.Add(ucSelectedProduct);
        }

        private void btnStockers_Click(object sender, RoutedEventArgs e)
        {
            spMain.Children.Clear();
            UcDisplayStocker ucDisplayStocker = new UcDisplayStocker(accountLogin);
            spMain .Children.Add(ucDisplayStocker);
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            spMain .Children.Clear();
            UcImportInventory ucImportInventory = new UcImportInventory(accountLogin);
            spMain.Children.Add(ucImportInventory);    
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            spMain.Children.Clear();
            UcExportInventory ucExportInventory = new UcExportInventory(accountLogin);
            spMain.Children.Add(ucExportInventory);    
        }
    }
}