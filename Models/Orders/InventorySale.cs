using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class InventorySale
    {
        public string IdProduct {  get; set; }
        public int QuantityInvoice {  get; set; }
        public double PriceOutput { get; set; }
        public int QuantitySold {  get; set; }
        public int Remaining {  get; set; }
        public Product product { get; set; }
        public ImportExport export { get; set; }

        public InventorySale() { }

        public InventorySale(int quantitySold, int remaining, ImportExport export, Product product)
        {
            this.export = export;
            IdProduct = export.product.Id;
            QuantityInvoice = export.Quantity;
            PriceOutput = export.product.PriceOutput;
            QuantitySold = quantitySold;
            Remaining = remaining;
            this.product = product;
        }
    }
}
