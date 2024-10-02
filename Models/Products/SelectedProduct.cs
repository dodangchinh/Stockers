using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class SelectedProduct
    {
        public int nProduct {  get; set; }  
        public Product product { get; set; }   
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public SelectedProduct() { }
        public SelectedProduct(Product product)
        {
            this.product = product;
        }
        public SelectedProduct(int nProduct, Product product)
        {
            this.nProduct = nProduct;
            this.product = product;
        }
    }
}
