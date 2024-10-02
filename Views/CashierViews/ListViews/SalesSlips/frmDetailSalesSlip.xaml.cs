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
using System.Windows.Shapes;

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for frmDetailSalesSlip.xaml
    /// </summary>
    public partial class frmDetailSalesSlip : Window
    {
        public frmDetailSalesSlip(List<SalesSlipDetail> latSalesSlipDetail)
        {
            InitializeComponent();

            dgSalesSlipDetails.ItemsSource = latSalesSlipDetail;
        }
    }
}
