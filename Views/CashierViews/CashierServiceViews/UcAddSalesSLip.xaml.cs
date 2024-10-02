using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UcAddSalesSLip.xaml
    /// </summary>
    public partial class UcAddSalesSLip : UserControl
    {
        double Discount = 0;
        double scoreUse = 0;

        Account accountLogin;
        Customer customer;
        SelectedProductService selectedProductService;
        InventorySaleService inventorySaleService;
        ExportDateService exportDateService;
        SaleSlipService saleSlipService;
        CustomerService customerService;
        CardService cardService;
        OutOfStockService outOfStockService;
        public UcAddSalesSLip(Account accountLogin)
        {
            InitializeComponent();
            InitVarible();

            this.accountLogin = accountLogin;
            SelectionCustomer(new object(), new EventArgs());
        }

        private void InitVarible()
        {
            customer = new Customer();
            selectedProductService = new SelectedProductService();
            inventorySaleService = new InventorySaleService();
            exportDateService = new ExportDateService();
            saleSlipService = new SaleSlipService();
            customerService = new CustomerService();
            cardService = new CardService();
            outOfStockService = new OutOfStockService();
        }

        private void SelectionCustomer(object sender, EventArgs e)
        {
            spMain.Children.Clear();
            UcSelectedCustomer ucSelectedCustomer = new UcSelectedCustomer();
            ucSelectedCustomer.delegateCustomer += GetCustomer;
            ucSelectedCustomer.Next += SelectionInventorySales;
            spMain.Children.Add(ucSelectedCustomer);
        }

        private Customer GetCustomer()
        {
            return customer;
        }

        private void SelectionInventorySales(object sender, EventArgs e)
        {
            spMain.Children.Clear();
            UcSelectedInventorySales ucSelectedInventorySales = new UcSelectedInventorySales();
            ucSelectedInventorySales.delegateSelectedProductService += GetSelectedProductService;
            ucSelectedInventorySales.Add += UcSelectedCategory_Add;
            spMain.Children.Add(ucSelectedInventorySales);
        }

        private void UcSelectedCategory_Add(object sender, EventArgs e)
        {
            ChangeTotalPay();
            CheckBtnContinue();
            spSelected.Children.Clear();
            UcDisplaySelected ucDisplaySelected = new UcDisplaySelected();
            ucDisplaySelected.delegateSelectedProductService += GetSelectedProductService;
            ucDisplaySelected.Deleted += CheckBtnContinue;
            spSelected.Children.Add(ucDisplaySelected);
        }

        private void CheckBtnContinue(object sender, EventArgs e)
        {
            ChangeTotalPay();
            CheckBtnContinue();
        }
        private SelectedProductService GetSelectedProductService()
        {
            return selectedProductService;
        }

        private void ChangeTotalPay()
        {
            lScore.Content += $"{customer.TotalScore.ToString("N0")}";
            txtTotalPay.Text = $"{selectedProductService.getTotalPayOut().ToString("N0")}";
        }

        private void Pay(object sender, EventArgs e)
        {
            Customer customer = customerService.GetByName(this.customer.Name);
            List<SalesSlipDetail> lstSalesSlipDetails = new List<SalesSlipDetail>();

            foreach (var item in selectedProductService.lstSelectedProduct)
            {
                InventorySale inventorySale = inventorySaleService.GetByProduct(item.product.Id);   
                inventorySale.product.QuantitySale += item.nProduct;
                inventorySaleService.ChangeSale(inventorySale, item.nProduct);

                SalesSlipDetail salesSlipDetail = new SalesSlipDetail(item.nProduct, inventorySale.product.GetPriceDiscount() * item.nProduct, inventorySale.product);
                lstSalesSlipDetails.Add(salesSlipDetail);

                int quantity = item.nProduct;

                while (inventorySale.product is Food)
                {
                    exportDateService.ChangeExpired(inventorySale.product, ref quantity);

                    if (quantity == 0)
                        break;
                }            
            }
            
            SalesSlip salesSlip = new SalesSlip($"PBH0{++Parameter.nSaleSlip}", customer.Cards.Id, accountLogin.Name, customer.Name, customer.PhoneNumber, customer.Address, Discount, lstSalesSlipDetails);
            saleSlipService.Add(salesSlip);

            CustomerDetail customerDetail = new CustomerDetail(salesSlip.Id, salesSlip);
            customer.lstDetails.Add(customerDetail);
            customer.TotalScore = customerService.getTotalScore(customer, scoreUse);
            customer.Cards.Score = customer.TotalScore;

            customerService.Update(customer);
            cardService.Update(customer.Cards);
            outOfStockService.checkUpdateRemaining(inventorySaleService.Gets());
            inventorySaleService.UpdateLstExport(inventorySaleService.Gets());
            exportDateService.UpdateList(exportDateService.Gets());
            MessageBox.Show("Successful!");
            spSelected.Children.Clear();
            ChangeTotalPay();
            CheckBtnContinue();

            frmDetailSalesSlip frmDetailSalesSlip = new frmDetailSalesSlip(salesSlip.lstDetail);
            frmDetailSalesSlip.ShowDialog();

            spMain.Children.Clear();
            spSelected.Children.Clear();
            txtScore.IsReadOnly = true;
            btnPay.IsEnabled = false;
        }


        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            frmAccept frmAccept = new frmAccept("Do you want to Pay?");
            frmAccept.Accept += Pay;
            frmAccept.ShowDialog();
        }

        private void CheckBtnContinue()
        {
            if (selectedProductService.CheckQuantitySelected())
            {
                txtScore.IsReadOnly = false;
                btnPay.IsEnabled = true;
            }             
            else
            {
                txtScore.IsReadOnly = true;
                btnPay.IsEnabled = false;
            }               
        }

        private void txtScore_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (txtScore.Text.Length <= 0)
            {
                txtDiscount.Text = "";
                return;
            }

            scoreUse = Convert.ToDouble(txtScore.Text);
            Discount = scoreUse * 10;
            txtDiscount.Text = Discount.ToString();
        }

        private void TxtScore_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static bool IsTextAllowed(string text)
        {
            return !new Regex("[^0-9]+").IsMatch(text);
        }

    }
}
