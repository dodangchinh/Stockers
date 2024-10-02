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
    /// Interaction logic for frmDisplayOrderDetail.xaml
    /// </summary>
    public partial class frmImportExportReceiptsDetail : Window
    {
        public frmImportExportReceiptsDetail(List<Receipt> lstImportExportReceiptsDetail)
        {
            InitializeComponent();

            dgImportExportReceiptsDetails.ItemsSource = lstImportExportReceiptsDetail;
        }
    }
}
