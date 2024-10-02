using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for frmEditAccount.xaml
    /// </summary>
    public partial class frmEditProduct : Window, INotifyPropertyChanged
    {
        Account accountLogin;
        
        private Product _productSelected;
        public Product productSelected
        {
            get { return _productSelected; }
            set
            {
                _productSelected = value;
                OnPropertyChanged();
            }
        }

        // create delegate 
        public delegate Product MyDelegate();
        public event MyDelegate myDelegate;

        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        RemainingProduct remainingProduct;

        ProductService productService;
        RemainingProductService remainingProductService;

      
        // khi sử dụng phương thức Show
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (myDelegate != null)
            {
                productSelected = myDelegate();
                txtId.Text = productSelected.Id;
                txtName.Text = productSelected.Name;
                txtPriceIn.Text = productSelected.PriceInput.ToString("N0");
                txtPriceOut.Text = productSelected.PriceOutput.ToString("N0");
            }            
        }

        public frmEditProduct(Account accountLogin)
        {
            InitializeComponent();
            this.accountLogin = accountLogin;

            productService = new ProductService();
            remainingProductService = new RemainingProductService();

            this.DataContext = this;
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            // B2: call delegate
            if (myDelegate != null)
            {
                productSelected = myDelegate();

                Product product = productSelected.Clone();
                CheckUpdateProduct(product);
            }
        }

        void CheckUpdateProduct(Product product)
        {
            if (productSelected.PriceInput.CompareTo(double.Parse(txtPriceIn.Text)) == 0)
                return;
            
            productService.ChangeProduct(product);

            remainingProduct = remainingProductService.GetRemainingProduct(product.Id);

            if (remainingProduct.QuantityRemain == 0)
            {
                productService.ChangePrice(product, double.Parse(txtPriceIn.Text));
                productSelected.PriceInput = product.PriceInput;
                productSelected.PriceOutput = product.PriceOutput;  

                MessageBox.Show("Successful!");
                this.Close();
            }
            else
                MessageBox.Show($"The product '{product.Name}' is in stock!");

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtPriceIn_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (txtPriceIn.Text.Length <= 0)
            {
                txtPriceOut.Text = "";
                return;
            }
            double PriceIn;
            if (!double.TryParse(txtPriceIn.Text, out PriceIn))
                return;

            PriceIn = double.Parse(txtPriceIn.Text);
            double PriceOut = PriceIn + PriceIn * 0.1 + PriceIn * 0.3 + PriceIn * (Parameter.nAccount * 0.036);
            txtPriceOut.Text = PriceOut.ToString("N0");
        }

        private void txtPriceIn_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return !new Regex("[^0-9]+").IsMatch(text);
        }
    }
}
