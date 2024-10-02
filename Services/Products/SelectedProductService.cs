using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class SelectedProductService
    {
        public List<SelectedProduct> lstSelectedProduct {  get; set; }
        public bool isElectronic {  get; set; }
        public bool isPorcelain {  get; set; }
        public bool isFood {  get; set; }
        public SelectedProductService()
        {
            isElectronic = false;
            isPorcelain = false;
            isFood = false;
            lstSelectedProduct = new List<SelectedProduct>();
        }
        public List<SelectedProduct> GetByQuantity()           
        {
            List<SelectedProduct> lstSelectedProduct = new List<SelectedProduct>();
            foreach (var item in this.lstSelectedProduct)
                if (item.nProduct > 0)
                    lstSelectedProduct.Add(item);
            return lstSelectedProduct;
        }
        public SelectedProduct GetByProduct(Product product)
        {
            foreach (var item in lstSelectedProduct)
                if(item.product.Name.CompareTo(product.Name) == 0)  
                    return item;
            return null;
        }

        public void Inits(List<Product> lstProduct)
        {
            if(checkCategory(lstProduct[0]))
                return;
            foreach (var item in lstProduct)
                lstSelectedProduct.Add(new SelectedProduct(item));
            isCategory(lstProduct[0]);
        }

        private void isCategory(Product product)
        {
            if (product is Electronic)
                isElectronic = true;
            else if (product is Porcelain)
                isPorcelain = true;
            else if(product is Food)
                isFood = true;
        }

        private bool checkCategory(Product product)
        {
            if (product is Electronic)
                if(isElectronic)
                    return true;
            else if (product is Porcelain)
                if(isPorcelain)
                    return true;
            else if (product is Food)
                if(isFood)
                    return true;
            return false;
        }

        public bool CheckQuantitySelected()
        {
            foreach (var item in lstSelectedProduct)
                if(item.nProduct > 0)
                    return true;
            return false;
        }

        public double getTotalPayIn()
        {
            double totalPay = 0;
            foreach (var item in lstSelectedProduct)
                totalPay += item.product.PriceInput * item.nProduct;
            return totalPay;
        }

        public double getTotalPayOut()
        {
            double totalPay = 0;
            foreach (var item in lstSelectedProduct)
                totalPay += item.product.PriceOutput * item.nProduct;
            return totalPay;
        }
    }
}