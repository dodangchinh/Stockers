using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class RemainingProduct
    {
        public int Quantity {  get; set; }
        public int QuantityExpDate {  get; set; }
        public int QuantityRemain {  get; set; }
        public Product product { get; set; }
        public RemainingProduct() { }
        public RemainingProduct(int quantity, int quantityExpDate, int quantityRemain, Product product)
        {
            Quantity = quantity;
            this.product = product;
            QuantityExpDate = quantityExpDate;
            QuantityRemain = quantityRemain;
        }
    }
}
