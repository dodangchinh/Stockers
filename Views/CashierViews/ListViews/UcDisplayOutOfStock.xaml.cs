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

    public partial class UcDisplaOutOfStock : UserControl
    {
        OutOfStockService outOfStockService { get; set; }
        public ObservableCollection<OutOfStock> lstOutOfStock { get; set; }
        public UcDisplaOutOfStock()
        {
            InitializeComponent();

            outOfStockService = new OutOfStockService();
            lstOutOfStock = new ObservableCollection<OutOfStock>(outOfStockService.Gets());
            this.DataContext = this;
        }
    }
}
