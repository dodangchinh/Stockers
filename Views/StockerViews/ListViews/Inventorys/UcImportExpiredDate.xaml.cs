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
    public partial class UcImportExpiredDate : UserControl
    {
        ExpiredDateService importExpiredDateService;
        ImportDateService importExpirationDateService;

        public ObservableCollection<ExpDate> lstImportExpirationDate { get; set; }
        public ObservableCollection<ExpDate> lstImportExpiredDate { get; set; }
        public UcImportExpiredDate()
        {
            InitializeComponent();
            InitVarible();

            this.DataContext = this;
        }

        private void InitVarible()
        {
            importExpirationDateService = new ImportDateService();
            importExpiredDateService = new ExpiredDateService();
            lstImportExpirationDate = new ObservableCollection<ExpDate>(importExpirationDateService.Gets());
            lstImportExpiredDate = new ObservableCollection<ExpDate>(importExpiredDateService.GetListImport());
        }
    }
}
