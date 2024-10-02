using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class OutOfStock
    {
        public int Remaining {  get; set; }
        public Product product { get; set; }
        public OutOfStock() { }
        public OutOfStock(int remaining, Product product)
        {
            Remaining = remaining;
            this.product = product;
        }
    }
}
