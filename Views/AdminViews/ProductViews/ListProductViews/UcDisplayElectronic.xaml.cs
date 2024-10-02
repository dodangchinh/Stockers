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
    public partial class UcDisplayElectronic : UserControl
    {
        Account accountLogin;
        ProductService productService;
        public Product electronicSelected { get; set; }

        public ObservableCollection<Product> lstElectronic { get; set; }
        public UcDisplayElectronic(Account accountLogin, bool flag)
        {
            InitializeComponent();
            this.accountLogin = accountLogin;
            productService = new ProductService();
            lstElectronic = new ObservableCollection<Product>(productService.GetElectronics());
            this.DataContext = this;

           CheckBtnVisibility(flag);
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

        private Product GetProduct()
        {
            if (electronicSelected == null)
                return null;

            var selected = electronicSelected;
            return selected;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmAddProduct frmAddProduct = new frmAddProduct(lstElectronic, "Electronic");
            frmAddProduct.DataContext = lstElectronic;
            frmAddProduct.ShowDialog();
        }
    }
    
}
