using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class RemainingProductService
    {
        public RemainingProductService()
        {
        }

        public void Add(RemainingProduct item)
        {
            UnitOfWork.Instance.remainingProductRepository.Add(item);
        }
        public void Update(RemainingProduct item, int quantity)
        {
            item.Quantity -= quantity;
            item.QuantityRemain -= quantity;
            UnitOfWork.Instance.remainingProductRepository.Update(item);
        }
        public RemainingProduct GetRemainingProduct(string productId)
        {
            foreach (var item in UnitOfWork.Instance.remainingProductRepository.Gets())
                if(item.product.Id.ToLower() == productId.ToLower())
                    return item;
            return null;
        }
        public void ChangeImport(RemainingProduct remainingProduct, int Quantity)
        {
            remainingProduct.Quantity += Quantity;
            remainingProduct.QuantityRemain += Quantity;
        }

        public void ChangeExport(RemainingProduct remainingProduct, int Quantity)
        {
            remainingProduct.Quantity -= Quantity;
        }
        public void Update(RemainingProduct remainingProduct)
        {
            UnitOfWork.Instance.remainingProductRepository.Update(remainingProduct);
        }

        public void UpdateList(List<RemainingProduct> remainingProducts)
        {
            foreach (var item in remainingProducts)
                UnitOfWork.Instance.remainingProductRepository.Update(item);
        }

        public List<RemainingProduct> Gets()
        {
            return UnitOfWork.Instance.remainingProductRepository.Gets();
        }

        public List<RemainingProduct> GetRemainings()
        {
            List<RemainingProduct> lstRemainingProduct = new List<RemainingProduct>();  
            foreach (var item in UnitOfWork.Instance.remainingProductRepository.Gets())
                if (item.QuantityRemain > 0)
                    lstRemainingProduct.Add(item);
            return lstRemainingProduct;
        }

        public List<RemainingProduct> GetRemainings(List<ExpDate> lstImportDate)
        {
            List<RemainingProduct> lstRemainingProduct = new List<RemainingProduct>();
            foreach (var item in UnitOfWork.Instance.remainingProductRepository.Gets())
            { 
                if (item.QuantityRemain > 0)
                {
                    if (item.product is Food)
                        item.QuantityRemain = getQuantityImportDateGreater(getImportDate(lstImportDate, item.product));
                    lstRemainingProduct.Add(item);
                }                   
            }
            return lstRemainingProduct;
        }

        private List<ExpDate> getImportDate(List<ExpDate> lstImportDate, Product product)
        {
            List<ExpDate> lstImportDates = new List<ExpDate> ();
            foreach (var item in lstImportDate)
                if(product.Id.CompareTo(item.product.Id) == 0)
                    lstImportDates.Add(item);
            return lstImportDates;
        }

        private int getQuantityImportDateGreater(List<ExpDate> lstImportDate)
        {
            int quantity = 0;
            foreach (var item in lstImportDate)
                if(quantity < item.Quantity)
                    quantity = item.Quantity;   
            return quantity;
        }

        public List<Product> GetProducts()
        {
            List<Product> lstProduct = new List<Product>();
            foreach (var item in UnitOfWork.Instance.remainingProductRepository.Gets())
                if (item.QuantityRemain > 0)
                    lstProduct.Add(item.product);
            return lstProduct;
        }
    }
}
