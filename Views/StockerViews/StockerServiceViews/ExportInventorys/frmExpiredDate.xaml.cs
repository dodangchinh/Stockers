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
using System.Windows.Shapes;

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for frmExpiredDate.xaml
    /// </summary>
    public partial class frmExpiredDate : Window
    {
        public static ExpDate importDateSelected {  get; set; }   
        ImportDateService importExpirationDateService;

        public ObservableCollection<ExpDate> lstImportExpirationDate { get; set; }
        public frmExpiredDate(RemainingProduct remainingProduct, int quantity)
        {
            InitializeComponent();

            importExpirationDateService = new ImportDateService();
            lstImportExpirationDate = new ObservableCollection<ExpDate>(importExpirationDateService.GetsByQuantity(remainingProduct.product.Id, quantity));
            this.DataContext = this;
            dgImportExpirationDates.SelectionChanged += MouseDouble_Click;
        }

        private void MouseDouble_Click(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid && importDateSelected != null)
                this.Close();
        }
    }
}
