using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class ExportInventoryService
    {
        public ExportInventoryService()
        {
        }

        public void Add(ImportExport inventory)
        {
            UnitOfWork.Instance.exportInventoryRepository.Add(inventory);
        }

        public ImportExport Get(string idProduct)
        {
            foreach (var item in UnitOfWork.Instance.exportInventoryRepository.Gets())
                if(item.product.Id.ToLower() == idProduct.ToLower())
                    return item;
            return null;
        }

        public void Update(ImportExport importInventory)
        {
            UnitOfWork.Instance.exportInventoryRepository.Update(importInventory);
        }

        public void UpdateList(List<ImportExport> lstImportInventory)
        {
            foreach (var item in lstImportInventory)
                UnitOfWork.Instance.importInventoryRepository.Update(item);
        }

        public List<ImportExport> Gets()
        {
            return UnitOfWork.Instance.exportInventoryRepository.Gets();    
        }


        public void Change(ImportExport importInventory, int Recent, double AmountRecent)
        {
            importInventory.Previous += importInventory.Recent;
            importInventory.AmountPre += importInventory.AmountRecent;
            importInventory.Recent = Recent;
            importInventory.AmountRecent = Recent * AmountRecent;
            importInventory.Quantity = importInventory.Previous + importInventory.Recent;
            importInventory.Total = importInventory.AmountPre + importInventory.AmountRecent;
            importInventory.ReceiptDate = DateTime.Now;
        }
    }
}
