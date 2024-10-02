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

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for UcDisplaySelected.xaml
    /// </summary>
    public partial class UcDisplaySelected : UserControl
    {
        public event EventHandler Deleted;

        public delegate SelectedProductService DelegateSelectedProductService();
        public DelegateSelectedProductService delegateSelectedProductService;

        public SelectedProduct selectedProduct {  get; set; }
        SelectedProductService selectedProductService {  get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (delegateSelectedProductService == null)
                return;
            selectedProductService = delegateSelectedProductService();
            dgProducts.ItemsSource = selectedProductService.GetByQuantity();
        }

        public UcDisplaySelected()
        {
            InitializeComponent();       
            this.DataContext = this;
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            selectedProduct.nProduct--;
            dgProducts.ItemsSource = selectedProductService.GetByQuantity();
            Deleted?.Invoke(this, EventArgs.Empty); 
        }
    }
}
