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
    /// Interaction logic for UcDisplaySalesSlip.xaml
    /// </summary>
    public partial class UcDisplaySalesSlip : UserControl
    {
        public SalesSlip salesSlipSelected {  get; set; }

        SaleSlipService saleSlipService { get; set; }
        public ObservableCollection<SalesSlip> lstSalesSlip { get; set; }
        public UcDisplaySalesSlip()
        {
            InitializeComponent();

            saleSlipService = new SaleSlipService();
            lstSalesSlip = new ObservableCollection<SalesSlip>(saleSlipService.Gets());

            this.DataContext = this;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            frmDetailSalesSlip frmDetailSalesSlip = new frmDetailSalesSlip(salesSlipSelected.lstDetail);
            frmDetailSalesSlip.ShowDialog();
        }
    }
}
