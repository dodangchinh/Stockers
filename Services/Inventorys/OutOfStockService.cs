using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class OutOfStockService
    {
        public OutOfStockService()
        {
        }

        public void Add(OutOfStock outStock)
        {
            UnitOfWork.Instance.outOfStockRepository.Add(outStock);
        }

        public void Update(OutOfStock outStock)
        {
            UnitOfWork.Instance.outOfStockRepository.Update(outStock);
        }

        public void Delete(OutOfStock outStock)
        {
            UnitOfWork.Instance.outOfStockRepository.Delete(outStock);
        }

        public OutOfStock SreachByProduct(Product product)
        {
            foreach (var item in UnitOfWork.Instance.outOfStockRepository.Gets())
                if(item.product.Id.CompareTo(product.Id) == 0)
                    return item;
            return null;
        }

        public bool isExist(Product product)
        {
            if(SreachByProduct(product) != null) 
                return true;
            return false;
        }

        public void checkUpdateRemaining(List<InventorySale> inventorySales)
        {
            foreach (var itemIS in inventorySales)
            {
                if(itemIS.Remaining < Parameter.OutOfStock)
                {
                    OutOfStock outOfStock = SreachByProduct(itemIS.product);
                    if(outOfStock != null)
                    {
                        outOfStock.Remaining = itemIS.Remaining;
                        Update(outOfStock);
                    }
                    else
                    {
                        outOfStock = new OutOfStock(itemIS.Remaining ,itemIS.product);
                        Add(outOfStock);
                    }
                }
                else
                {
                    OutOfStock outOfStock = SreachByProduct(itemIS.product);
                    if (outOfStock != null)
                        Delete(outOfStock);
                }
            }
        }

        public List<OutOfStock> Gets()
        {
            return UnitOfWork.Instance.outOfStockRepository.Gets();
        }
    }
}
