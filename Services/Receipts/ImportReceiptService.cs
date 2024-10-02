using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class ImportReceiptService
    {
        public ImportReceiptService()
        {
        }

        public void Add(ImportExportReceipt importReceipt)
        {
            importReceipt.Total = GetTotal(importReceipt);
            UnitOfWork.Instance.importReceiptRepository.Add(importReceipt);
        }

        public ImportExportReceipt GetById(string id)
        {
            foreach (var item in UnitOfWork.Instance.importReceiptRepository.Gets())
                if(item.Id.ToLower().CompareTo(id.ToLower()) == 0)
                    return item;
            return null;
        }

        public int GetTotal(ImportExportReceipt importReceipt)
        {
            int Total = 0;
            foreach (var item in importReceipt.lstReceipts)
                Total += item.Quantity;
            return Total;
        }

        public List<ImportExportReceipt> Gets()
        {
            return UnitOfWork.Instance.importReceiptRepository.Gets();
        }
    }
}
