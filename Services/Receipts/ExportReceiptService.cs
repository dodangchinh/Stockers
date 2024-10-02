using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class ExportReceiptService
    {
        public ExportReceiptService()
        {
        }

        public void Add(ImportExportReceipt importReceipt)
        {
            importReceipt.Total = GetTotal(importReceipt);
            UnitOfWork.Instance.exportReceiptRepository.Add(importReceipt);
        }

        public void AddList(List<ImportExportReceipt> importReceipts)
        {
            foreach (var item in importReceipts)
            {
                item.Total = GetTotal(item);
                UnitOfWork.Instance.exportReceiptRepository.Add(item);
            }         
        }

        public ImportExportReceipt GetById(string id)
        {
            foreach (var item in UnitOfWork.Instance.exportReceiptRepository.Gets())
                if (item.Id.ToLower().CompareTo(id.ToLower()) == 0)
                    return item;
            return null;
        }

        public int GetTotal(ImportExportReceipt exportReceipt)
        {
            int Total = 0;
            foreach (var item in exportReceipt.lstReceipts)
                Total += item.Quantity;
            return Total;
        }

        public List<ImportExportReceipt> Gets()
        {
            return UnitOfWork.Instance.exportReceiptRepository.Gets();  
        }
    }
}
