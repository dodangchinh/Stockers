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
    public partial class UcDisplayImportReceipts : UserControl
    {
        Account accountLogin;
        ImportReceiptService importReceiptService;

        public ImportExportReceipt importReceiptSelected { get; set; }

        public ObservableCollection<ImportExportReceipt> lstImportReceipt { get; set; }
        public UcDisplayImportReceipts(Account accountLogin)
        {
            InitializeComponent();

            this.accountLogin = accountLogin;
            importReceiptService = new ImportReceiptService();
            lstImportReceipt = new ObservableCollection<ImportExportReceipt>(importReceiptService.Gets());
            this.DataContext = this;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            frmImportExportReceiptsDetail frmImportExportReceiptsDetail = new frmImportExportReceiptsDetail(importReceiptSelected.lstReceipts);
            frmImportExportReceiptsDetail.ShowDialog();
        }
    }
}
