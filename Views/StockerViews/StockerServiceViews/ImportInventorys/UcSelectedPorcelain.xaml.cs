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
    public partial class UcSelectedPorcelain : UserControl
    {
        public event EventHandler Add;

        public delegate SelectedProductService DelegateSelectedProductService();
        public event DelegateSelectedProductService delegateSelectedProductService;

        Account accountLogin;
        ProductService porcelainService;
        public static SelectedProduct selectedProduct { get; set; }
        public static SelectedProductService selectedProductService { get; set; }

        public Product porcelainSelected { get; set; }
        public ObservableCollection<Product> lstPorcelain { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (delegateSelectedProductService == null)
                return;

            selectedProductService = delegateSelectedProductService();
        }
        public UcSelectedPorcelain(Account accountLogin)
        {
            InitializeComponent();
            InitVarible();

            this.accountLogin = accountLogin;
            this.DataContext = this;
        }

        private void InitVarible()
        {
            porcelainService = new ProductService();
            selectedProductService = new SelectedProductService();
            lstPorcelain = new ObservableCollection<Product>(porcelainService.GetPorcelains());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            selectedProductService.Inits(porcelainService.GetPorcelains());
            selectedProduct = selectedProductService.GetByProduct(porcelainSelected);
            selectedProduct.nProduct++;
            Add?.Invoke(this, EventArgs.Empty);
        }
    }
}