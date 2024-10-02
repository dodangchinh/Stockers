using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for UcDisplayAccount.xaml
    /// </summary>
    public partial class UcDisplayPorcelain : UserControl
    {
        Account accountLogin;
        ProductService porcelainService;
        public Product porcelainSelected { get; set; }

        public ObservableCollection<Product> lstPorcelain { get; set; }
        public UcDisplayPorcelain(Account accountLogin, bool flag)
        {
            InitializeComponent();
            CheckBtnVisibility(flag);

            this.accountLogin = accountLogin;
            porcelainService = new ProductService();
            lstPorcelain = new ObservableCollection<Product>(porcelainService.GetPorcelains());
            this.DataContext = this;
        }

        private void CheckBtnVisibility(bool flag)
        {
            if (flag)
                return;

            BtnEditColumn.Visibility = Visibility.Collapsed;
            btnAdd.Visibility = Visibility.Collapsed;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            frmEditProduct frmEditProduct = new frmEditProduct(accountLogin);
            frmEditProduct.myDelegate += GetProduct;
            frmEditProduct.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddProduct frmAddProduct = new frmAddProduct(lstPorcelain, "Porcelain");
            frmAddProduct.DataContext = lstPorcelain;
            frmAddProduct.ShowDialog();
        }

        private Product GetProduct()
        {
            if (porcelainSelected == null)
                return null;

            var selected = porcelainSelected;
            return selected;
        }
    }
}
