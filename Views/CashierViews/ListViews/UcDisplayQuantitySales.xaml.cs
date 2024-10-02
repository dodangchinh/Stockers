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

    public partial class UcDisplaQuantitySales : UserControl
    {
        ProductService productService;
        public ObservableCollection<Product> lstProduct { get; set; }
        public UcDisplaQuantitySales()
        {
            InitializeComponent();

            productService = new ProductService();
            lstProduct = new ObservableCollection<Product>(UnitOfWork.Instance.lstProduct);
            this.DataContext = this;

            txtRevenue.Text = productService.GetRevenue(UnitOfWork.Instance.lstProduct).ToString("N0");
            txtProfit.Text = productService.GetProfit(UnitOfWork.Instance.lstProduct).ToString("N0");   
        }
    }
}
