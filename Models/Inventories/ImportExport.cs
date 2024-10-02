using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class ImportExport
    {
        public int Previous {  get; set; }   
        public double AmountPre {  get; set; }
        public int Recent {  get; set; }
        public double AmountRecent {  get; set; }
        public int Quantity {  get; set; }
        public double Total {  get; set; }
        public Product product { get; set; }
        public DateTime ReceiptDate { get; set; } = DateTime.Now;

        public ImportExport(Product product)
        {
            this.product = product;
        }
        public ImportExport() { }

        public ImportExport(int previous, double amountPre, int recent, double amountRecent, int quantity, double total, Product product, DateTime dateTime)
        {
            Previous = previous;
            AmountPre = amountPre;
            Recent = recent;
            AmountRecent = amountRecent;
            Quantity = quantity;
            Total = total;
            this.product = product;
            ReceiptDate = dateTime;
        }

        public ImportExport Clone()
        {
            return new ImportExport(Previous, AmountPre, Recent, AmountRecent, Quantity, Total, product, ReceiptDate);
        }
    }
}
