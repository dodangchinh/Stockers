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
    public partial class UcDisplayCustomers : UserControl
    {
        public Customer customerSelected {  get; set; }
        CustomerService customerService { get; set; }
        public ObservableCollection<Customer> lstCustomer { get; set; }
        public UcDisplayCustomers()
        {
            InitializeComponent();

            customerService = new CustomerService();
            lstCustomer = new ObservableCollection<Customer>(customerService.Gets());
            this.DataContext = this;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            frmDetailCustomer frmDetailCustomer = new frmDetailCustomer(customerSelected.lstDetails);
            frmDetailCustomer.ShowDialog();
        }
    }
}
