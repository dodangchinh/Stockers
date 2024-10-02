using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class ImportInventoryService
    {

        public ImportInventoryService()
        {
        }

        public void Add(ImportExport inventory)
        {
            UnitOfWork.Instance.importInventoryRepository.Add(inventory);
        }

        public ImportExport Get(string idProduct)
        {
            foreach (var item in UnitOfWork.Instance.importInventoryRepository.Gets())
                if(item.product.Id.ToLower() == idProduct.ToLower())
                    return item;
            return null;
        }

        public void Update(ImportExport importInventory)
        {
            UnitOfWork.Instance.importInventoryRepository.Update(importInventory);
        }

        public void UpdateList(List<ImportExport> lstImportInventory)
        {
            foreach (var item in lstImportInventory)
                UnitOfWork.Instance.importInventoryRepository.Update(item);
        }

        public List<ImportExport> Gets()
        {
            return UnitOfWork.Instance.importInventoryRepository.Gets();    
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
