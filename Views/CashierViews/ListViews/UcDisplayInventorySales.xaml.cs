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

    public partial class UcDisplayInventorySales : UserControl
    {
        InventorySaleService inventorySaleService;
        public ObservableCollection<InventorySale> lstInventorySale { get; set; }
        public UcDisplayInventorySales()
        {
            InitializeComponent();

            inventorySaleService = new InventorySaleService();
            lstInventorySale = new ObservableCollection<InventorySale>(inventorySaleService.GetListRemaining());
            this.DataContext = this; 
        }
    }
}
