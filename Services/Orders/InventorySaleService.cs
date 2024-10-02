using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class InventorySaleService
    {
        public InventorySaleService()
        {
        }

        public void Add(InventorySale item)
        {
            UnitOfWork.Instance.inventorySaleRepository.Add(item);
        }
        public void ChangeExport(InventorySale item, int Quantity)
        {
            item.QuantityInvoice += Quantity;
            item.Remaining += Quantity;
        }
        public void UpdateExport(InventorySale item)
        {
            UnitOfWork.Instance.inventorySaleRepository.Update(item);
        }
        public void UpdateLstExport(List<InventorySale> lstInventorySale)
        {
            foreach (var item in lstInventorySale)
                UnitOfWork.Instance.inventorySaleRepository.Update(item);
        }

        public void ChangeSale(InventorySale item, int Quantity)
        {
            item.QuantitySold += Quantity;
            item.Remaining -= Quantity;
        }

        public InventorySale GetByProduct(string id)
        {
            foreach (var item in UnitOfWork.Instance.inventorySaleRepository.Gets())
                if(item.IdProduct.ToLower().CompareTo(id.ToLower()) == 0)
                    return item;
            return null;
        }

        public InventorySale GetRemaining(int no)
        {
            int i = 0;
            foreach (var item in UnitOfWork.Instance.inventorySaleRepository.Gets())
            {
                if (item.Remaining > 0)
                {
                    i++;
                    if (i == no)
                        return item;
                }
            }             
            return null;
        }

        public bool checkRemaining(InventorySale item)
        {
            if (item.Remaining > Parameter.OutOfStock)
                return true;
            return false;
        }

        public List<InventorySale> Gets()
        {
            return UnitOfWork.Instance.inventorySaleRepository.Gets();  
        }

        public List<InventorySale> GetListRemaining()
        {
            List<InventorySale> lstInventorySale = new List<InventorySale>();
            foreach (var item in UnitOfWork.Instance.inventorySaleRepository.Gets())
                if(item.Remaining > 0) 
                    lstInventorySale.Add(item);
            return lstInventorySale;
        }

        public List<Product> GetProducts()
        {
            List<Product> lstProduct = new List<Product>();
            foreach (var item in UnitOfWork.Instance.inventorySaleRepository.Gets())
                if (item.Remaining > 0)
                    lstProduct.Add(item.product);
            return lstProduct;
        }
    }
}
