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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chinh_QuanLyKho
{
    /// <summary>
    /// Interaction logic for UcImportInventory.xaml
    /// </summary>
    public partial class UcImportInventory : UserControl
    {
        Account accountLogin;
        ProductService foodService;
        ImportInventoryService importedInventoryService;
        RemainingProductService remainingProductService;
        ImportReceiptService importReceiptService;
        ImportDateService importDateService;
        SelectedProductService selectedProductService;
        public UcImportInventory(Account accountLogin)
        {
            InitializeComponent();
            InitVariable();

            this.accountLogin = accountLogin;
            SelectedProduct();      
        }

        private void InitVariable()
        {
            foodService = new ProductService();
            selectedProductService = new SelectedProductService();
            importedInventoryService = new ImportInventoryService();
            remainingProductService = new RemainingProductService();
            importReceiptService = new ImportReceiptService();
            importDateService = new ImportDateService();
        }

        private void SelectedProduct()
        {
            UcSelectedCategory ucSelectedCategory = new UcSelectedCategory(accountLogin);
            ucSelectedCategory.delegateSelectedProductService += GetSelectedProductService;
            ucSelectedCategory.Add += UcSelectedCategory_Add;
            spMain.Children.Add(ucSelectedCategory);
        }

        private SelectedProductService GetSelectedProductService()
        {
            return selectedProductService;
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

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            frmAccept frmAccept = new frmAccept("Do tou want to Import Inventory?");
            frmAccept.Accept += UcSelectedCategory_Import;
            frmAccept.ShowDialog();
        }

        private void UcSelectedCategory_Import(object sender, EventArgs e)
        {
            int n = 0;
            string Id = $"PNK0{++Parameter.nImportReceipt}";
            string Name = accountLogin.Name;

            ImportExportReceipt importReceipt = new ImportExportReceipt();
            List<ExpDate> lstExpDate = new List<ExpDate>();
            foreach (var item in selectedProductService.lstSelectedProduct)
            {
                if (item.nProduct == 0)
                    continue;

                ImportExport import = importedInventoryService.Get(item.product.Id);
                importedInventoryService.Change(import, item.nProduct, item.product.PriceInput);

                RemainingProduct remainingProduct = remainingProductService.GetRemainingProduct(item.product.Id);
                remainingProductService.ChangeImport(remainingProduct, item.nProduct);
                importReceipt.lstReceipts.Add(new Receipt(++n, item.nProduct, item.product));

                if (!(item.product is Food))
                    continue;

                frmSelectedDate frmSelectedDate = new frmSelectedDate(item);
                frmSelectedDate.ShowDialog();
                string id;
                while (true)
                {
                    id = $"I0{++Parameter.nImportDate}";
                    if (importDateService.GetById(id) == null)
                        break;
                }
                ExpDate importDate = new ExpDate(id, Id, item.nProduct, 0, item.product, item.start, item.end);
                lstExpDate.Add(importDate);
            }

            importReceipt.Id = Id;
            importReceipt.Name = Name;
            importReceiptService.Add(importReceipt);
            importedInventoryService.UpdateList(importedInventoryService.Gets());
            remainingProductService.UpdateList(remainingProductService.Gets());

            if(lstExpDate.Count > 0)
                importDateService.AddList(lstExpDate);
            MessageBox.Show("Successful!");
            spMain.Children.Clear();
            spSelected.Children.Clear();
            UcDisplayImportReceipt ucDisplayImportReceipt = new UcDisplayImportReceipt(importReceipt);
            spMain.Children.Add(ucDisplayImportReceipt);

            btnImport.IsEnabled = false;
        }

        private void CheckBtnContinue()
        {
            if(selectedProductService.CheckQuantitySelected())
                btnImport.IsEnabled = true;
            else
                btnImport.IsEnabled = false;
        }

        private void ChangeTotalPay()
        {
            txtTotalPay.Text = $"{selectedProductService.getTotalPayIn().ToString("N0")}";
        }
    }
}