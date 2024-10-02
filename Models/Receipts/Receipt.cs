using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class Receipt
    {
        public int Quantity { get; set; }
        public Product product { get; set; }

        public Receipt() { }
        public Receipt(int no, int quantity, Product product)
        {
            Quantity = quantity;
            this.product = product;
        }
    }
}
