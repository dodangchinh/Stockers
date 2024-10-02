using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chinh_QuanLyKho
{
    public static class Validator
    {  
        static AccountService accountService = new AccountService();
        static AccountRoleService accountRoleService = new AccountRoleService();    
        static ProductService productService = new ProductService();
        static ImportDateService importDateService = new ImportDateService();
        static ExpiredDateService expiredDateService = new ExpiredDateService();
        static ExportDateService exportDateService = new ExportDateService(); 
        static RemainingProductService remainingProductService = new RemainingProductService();
        static InventorySaleService inventorySaleService = new InventorySaleService();
        static SaleSlipService saleSlipService = new SaleSlipService();

        public static void CheckAll()
        {
            UpdateExpDate();
            productService.UpdateQuantitySale(productService.Gets(), saleSlipService.Gets());
        }

        private static void UpdateExpDate()
        {
            List<ExpDate> lstImportDate = UnitOfWork.Instance.importDateRepository.Gets();
            for (int i = 0; i < lstImportDate.Count; i++)
            {
                if (lstImportDate[i].ExpDates < DateTime.Now)
                {
                    expiredDateService.Add(lstImportDate[i]);
                    RemainingProduct remainingProduct = remainingProductService.GetRemainingProduct(lstImportDate[i].product.Id);
                    remainingProduct.QuantityExpDate += lstImportDate[i].Quantity;
                    remainingProductService.Update(remainingProduct, lstImportDate[i].Quantity);
                    importDateService.Delete(lstImportDate[i]);
                    if (i >= lstImportDate.Count)
                        break;
                    i--;
                }
            }

            List<ExpDate> lstExportDate = UnitOfWork.Instance.exportDateRepository.Gets();
            for (int i = 0; i < lstExportDate.Count; i++)
            {
                if (lstExportDate[i].ExpDates < DateTime.Now)
                {
                    expiredDateService.Add(lstExportDate[i]);
                    InventorySale inventorySale = inventorySaleService.GetByProduct(lstExportDate[i].product.Id);
                    inventorySale.QuantityInvoice -= lstExportDate[i].Quantity;
                    if (inventorySale.QuantityInvoice > 0)
                        inventorySale.Remaining -= lstExportDate[i].Quantity;
                    else if (inventorySale.Remaining == 0)
                        inventorySale.Remaining = 0;
                    inventorySaleService.UpdateExport(inventorySale);
                    exportDateService.Delete(lstExportDate[i]);
                    if (i >= lstExportDate.Count)
                        break;
                    i--;
                }
            }

        }
    }
}
