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
    /// Interaction logic for UcExportInventory.xaml
    /// </summary>
    public partial class UcExportInventory : UserControl
    {
        Account accountLogin;

        ImportExportReceipt exportReceipt;
        ExpDate importDate;

        SelectedProductService selectedProductService;
        ImportDateService importDateService;
        ImportInventoryService importedInventoryService;
        RemainingProductService remainingProductService;
        ImportReceiptService importReceiptService;
        ExportInventoryService exportInventoryService;
        InventorySaleService inventorySaleService;
        ExportReceiptService exportReceiptService;
        OutOfStockService outOfStockService;
        ExportDateService exportDateService;
        public UcExportInventory(Account accountLogin)
        {
            InitializeComponent();
            InitVariable();

            this.accountLogin = accountLogin;
            SelectedProduct();
        }

        private void InitVariable()
        {
            selectedProductService = new SelectedProductService();
            importDateService = new ImportDateService();
            importedInventoryService = new ImportInventoryService();
            remainingProductService = new RemainingProductService();
            importReceiptService = new ImportReceiptService();
            exportInventoryService = new ExportInventoryService();
            inventorySaleService = new InventorySaleService();
            exportReceiptService = new ExportReceiptService();
            outOfStockService = new OutOfStockService();
            exportDateService = new ExportDateService();
        }

        private void SelectedProduct()
        {
            UcSelectedRemaining ucSelectedRemaining = new UcSelectedRemaining();
            ucSelectedRemaining.delegateSelectedProductService += GetSelectedProductService;
            ucSelectedRemaining.Add += UcSelectedCategory_Add;
            spMain.Children.Add(ucSelectedRemaining);   
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

        private SelectedProductService GetSelectedProductService()
        {
            return selectedProductService;
        }

        private void CheckBtnContinue(object sender, EventArgs e)
        {
            ChangeTotalPay();
            CheckBtnContinue();
        }

        private void CheckBtnContinue()
        {
            if (selectedProductService.CheckQuantitySelected())
                btnExport.IsEnabled = true;
            else
                btnExport.IsEnabled = false;
        }

        private void ChangeTotalPay()
        {
            txtTotalPay.Text = $"{selectedProductService.getTotalPayIn().ToString("N0")}";
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            frmAccept frmAccept = new frmAccept("Do you want to Export Inventory?");
            frmAccept.Accept += FrmAccept_Accept;
            frmAccept.ShowDialog();
        }

        private void FrmAccept_Accept(object sender, EventArgs e)
        {
            string Id = $"PXK0{++Parameter.nExportReceipt}";
            string Name = accountLogin.Name;
            exportReceipt = new ImportExportReceipt();
            exportReceipt.Id = Id;
            exportReceipt.Name = Name;

            foreach (var item in selectedProductService.lstSelectedProduct)
            {
                if (item.nProduct == 0)
                    continue;

                RemainingProduct remainingProduct = remainingProductService.GetRemainingProduct(item.product.Id);
                if (item.product is Food)
                {
                    frmExpiredDate.importDateSelected = null;
                    while (frmExpiredDate.importDateSelected == null)
                    {
                        frmExpiredDate frmExpiredDate = new frmExpiredDate(remainingProduct, item.nProduct);
                        frmExpiredDate.ShowDialog();
                    }
                    importDate = frmExpiredDate.importDateSelected;
                    importDateService.ChangeImport(importDate, item.nProduct);
                }
                    

                remainingProductService.Update(remainingProduct, item.nProduct);

                ImportExport export = exportInventoryService.Get(item.product.Id);
                exportInventoryService.Change(export, item.nProduct, item.product.PriceInput);
                exportReceipt.lstReceipts.Add(new Receipt(1, item.nProduct, item.product));

                InventorySale inventorySale = inventorySaleService.GetByProduct(item.product.Id);
                inventorySaleService.ChangeExport(inventorySale, item.nProduct);

                exportReceiptService.Add(exportReceipt);
                importDateService.UpdateList(importDateService.Gets());
                exportInventoryService.UpdateList(exportInventoryService.Gets());
                inventorySaleService.UpdateLstExport(inventorySaleService.Gets());
                outOfStockService.checkUpdateRemaining(inventorySaleService.Gets());

                if (item.product is Food)
                {
                    string id;
                    while (true)
                    {
                        id = $"E0{++Parameter.nExportDate}";
                        if (importDateService.GetById(id) == null)
                            break;
                    }
                    exportDateService.Add(new ExpDate(id, Id, item.nProduct, 0, importDate.product, importDate.MfgDate, importDate.ExpDates));
                }
            }
            MessageBox.Show("Successful!");
            spMain.Children.Clear();
            spSelected.Children.Clear();
            UcDisplayExportReceipt ucDisplayExportReceipt = new UcDisplayExportReceipt(exportReceipt);
            spMain.Children.Add(ucDisplayExportReceipt);
            btnExport.IsEnabled = false;
        }
    }
}