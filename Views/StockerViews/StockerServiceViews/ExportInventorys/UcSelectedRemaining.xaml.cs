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
    public partial class UcSelectedRemaining : UserControl
    {
        public event EventHandler Add;
        public delegate SelectedProductService DelegateSelectedProductService();
        public event DelegateSelectedProductService delegateSelectedProductService;

        List<ExpDate> lstImportDate;
        ImportDateService importDateService;
        RemainingProductService remainingProductService;

        SelectedProduct selectedProduct;
        SelectedProductService selectedProductService;
        public RemainingProduct remainingSelected {  get; set; }

        public ObservableCollection<RemainingProduct> lstRemainingInventory { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (delegateSelectedProductService == null)
                return;

            selectedProductService = delegateSelectedProductService();
        }
        public UcSelectedRemaining()
        {
            InitializeComponent();
            InitVarible();
            
            this.DataContext = this;
        }

        private void InitVarible()
        {
            importDateService = new ImportDateService();
            remainingProductService = new RemainingProductService();
            lstImportDate = importDateService.Gets();
            List<RemainingProduct> lstRemainingProduct = remainingProductService.GetRemainings(lstImportDate);
            lstRemainingInventory = new ObservableCollection<RemainingProduct>(lstRemainingProduct);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            selectedProductService.Inits(remainingProductService.GetProducts());
            selectedProduct = selectedProductService.GetByProduct(remainingSelected.product);
            if (selectedProduct.nProduct + 1 > remainingSelected.QuantityRemain)
                return;
            selectedProduct.nProduct++;
            Add?.Invoke(this, EventArgs.Empty);
        }
    }
}
