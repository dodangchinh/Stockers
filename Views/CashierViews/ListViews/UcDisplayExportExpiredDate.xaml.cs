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
    public partial class UcDisplayExportExpiredDate : UserControl
    {
        ExpiredDateService exportExpiredDateService;
        ExportDateService exportExpirationDateService;

        public ObservableCollection<ExpDate> lstExportExpirationDate { get; set; }
        public ObservableCollection<ExpDate> lstExportExpiredDate { get; set; }
        public UcDisplayExportExpiredDate()
        {
            InitializeComponent();
            InitVarible();

            this.DataContext = this;
        }

        private void InitVarible()
        {
            exportExpirationDateService = new ExportDateService();
            exportExpiredDateService = new ExpiredDateService();
            lstExportExpirationDate = new ObservableCollection<ExpDate>(exportExpirationDateService.Gets());
            lstExportExpiredDate = new ObservableCollection<ExpDate>(exportExpiredDateService.GetListExport());
        }
    }
}
