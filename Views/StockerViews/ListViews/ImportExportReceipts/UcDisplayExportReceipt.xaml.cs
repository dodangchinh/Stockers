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
    public partial class UcDisplayExportReceipt : UserControl
    {  
        public ImportExportReceipt exportReceiptSelected { get; set; }

        public ObservableCollection<ImportExportReceipt> lstExportReceipt { get; set; }
        public UcDisplayExportReceipt(ImportExportReceipt exportReceipt)
        {
            InitializeComponent();

            List<ImportExportReceipt> lstExportReceipt = new List<ImportExportReceipt>();
            lstExportReceipt.Add(exportReceipt);
            this.lstExportReceipt = new ObservableCollection<ImportExportReceipt>(lstExportReceipt);
            this.DataContext = this;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            frmImportExportReceiptsDetail frmImportExportReceiptsDetail = new frmImportExportReceiptsDetail(exportReceiptSelected.lstReceipts);
            frmImportExportReceiptsDetail.ShowDialog();
        }
    }
}
