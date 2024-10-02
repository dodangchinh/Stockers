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
    public partial class UcSelectedCustomer : UserControl
    {
        public event EventHandler Next;
        public delegate Customer DelegateCustomer();
        public event DelegateCustomer delegateCustomer;

        Customer customer;
        public static Customer customerSelected {  get; set; }
        CustomerService customerService { get; set; }
        public ObservableCollection<Customer> lstCustomer { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (delegateCustomer == null)
                return;

            customer = delegateCustomer();
        }
        public UcSelectedCustomer()
        {
            InitializeComponent();

            customerSelected = null;
            customerService = new CustomerService();
            lstCustomer = new ObservableCollection<Customer>(customerService.Gets());
            dgCustomers.SelectionChanged += MouseDouble_Click;
            this.DataContext = this;
        }
        private void MouseDouble_Click(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid && customerSelected != null)
            {
                ChangeCustomer();
                Next?.Invoke(this, EventArgs.Empty);
            }
               
        }

        private void ChangeCustomer()
        {
            customer.No = customerSelected.No;
            customer.Name = customerSelected.Name;
            customer.Address = customerSelected.Address;
            customer.PhoneNumber = customerSelected.PhoneNumber;
            customer.Cards = customerSelected.Cards;
            customer.TotalScore = customerSelected.TotalScore;
            customer.lstDetails = customerSelected.lstDetails;
        }
    }
}
