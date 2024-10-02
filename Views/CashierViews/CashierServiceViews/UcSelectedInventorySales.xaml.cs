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

    public partial class UcSelectedInventorySales : UserControl
    {
        public event EventHandler Add;
        public delegate SelectedProductService DelegateSelectedProductService();
        public event DelegateSelectedProductService delegateSelectedProductService;

        List<ExpDate> lstExportDate;
        ExportDateService exportDateService;
        SelectedProduct selectedProduct;
        SelectedProductService selectedProductService;

        public InventorySale inventorySalesSelected { get; set; }
        InventorySaleService inventorySaleService;

        public ObservableCollection<InventorySale> lstInventorySale { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (delegateSelectedProductService == null)
                return;

            selectedProductService = delegateSelectedProductService();
        }
        public UcSelectedInventorySales()
        {
            InitializeComponent();
            InitVarible();
           
            this.DataContext = this; 
        }

        private void InitVarible()
        {
            inventorySaleService = new InventorySaleService();
            List<InventorySale> lstInventorySales = inventorySaleService.GetListRemaining();
            lstInventorySale = new ObservableCollection<InventorySale>(lstInventorySales);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            selectedProductService.Inits(inventorySaleService.GetProducts());
            selectedProduct = selectedProductService.GetByProduct(inventorySalesSelected.product);
            if (selectedProduct.nProduct + 1 > inventorySalesSelected.Remaining)
                return;
            selectedProduct.nProduct++;
            Add?.Invoke(this, EventArgs.Empty);
        }
    }
}
