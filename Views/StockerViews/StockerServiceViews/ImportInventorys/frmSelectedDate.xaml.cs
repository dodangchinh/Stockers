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
    /// Interaction logic for frmSelectedDate.xaml
    /// </summary>
    public partial class frmSelectedDate : Window
    {
        SelectedProduct selectedProduct; 
        public frmSelectedDate(SelectedProduct selectedProduct)
        {
            InitializeComponent();
            this.selectedProduct = selectedProduct;
            lNameProduct.Content += $"Enter the date for {selectedProduct.product.Name}";
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            if(CheckValue())
                this.Close();
        }

        bool CheckValue()
        {
            if (manufactureDate.SelectedDate == null)
            {
                MessageBox.Show("Please select a Manufacture date!");
                return false;
            }
            else if (expirationDate.SelectedDate == null)
            {
                MessageBox.Show("Please select a Expiration date!");
                return false;
            }
            else
            {
                selectedProduct.start = manufactureDate.SelectedDate.Value;
                selectedProduct.end = expirationDate.SelectedDate.Value;

                if (selectedProduct.start.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Manufacture Date must be greater than Now Date! Select again!");
                    return false;
                }
                else if (selectedProduct.end.Date < selectedProduct.start.Date)
                {
                    MessageBox.Show("Expiration Date must be greater than Manufacture Date! Select again!");
                    return false;
                }
            }
            return true;
        }
    }
}
