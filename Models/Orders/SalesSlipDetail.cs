using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class SalesSlipDetail
    {
        public int Quantity {  get; set; }
        public double Discount {  get; set; }
        public Product product { get; set; }
        public SalesSlipDetail() { }
        public SalesSlipDetail(int quantity, double discount, Product product)
        {
            this.Quantity = quantity;
            this.Discount = discount;
            this.product = product;
        }
    }
}
