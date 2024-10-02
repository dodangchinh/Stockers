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
using System.Xml.Linq;

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for UcDisplayAccount.xaml
    /// </summary>
    public partial class UcSelectedElectronic : UserControl
    {
        public event EventHandler Add;

        public delegate SelectedProductService DelegateSelectedProductService();
        public event DelegateSelectedProductService delegateSelectedProductService;

        Account accountLogin;
        ProductService electronicService;

        public SelectedProduct selectedProduct {  get; set; }
        public SelectedProductService selectedProductService { get; set; }

        public Product electronicSelected { get; set; }
        public ObservableCollection<Product> lstElectronic { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (delegateSelectedProductService == null)
                return;

            selectedProductService = delegateSelectedProductService();
        }

        public UcSelectedElectronic(Account accountLogin)
        {
            InitializeComponent();
            InitVarible();

            this.accountLogin = accountLogin;
            this.DataContext = this;
        }

        private void InitVarible()
        {
            electronicService = new ProductService();
            lstElectronic = new ObservableCollection<Product>(electronicService.GetElectronics());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            selectedProductService.Inits(electronicService.GetElectronics());
            selectedProduct = selectedProductService.GetByProduct(electronicSelected);
            selectedProduct.nProduct++;
            Add?.Invoke(this, EventArgs.Empty);
        }
    }
}