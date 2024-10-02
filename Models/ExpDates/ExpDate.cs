using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class ExpDate
    {
        public string Id {  get; set; }    
        public string IdReceipt {  get; set; }
        public int Quantity {  get; set; }
        public int QuantityUsed { get; set; }
        public Product product { get; set; }
        public DateTime MfgDate { get; set; }
        public DateTime ExpDates {  get; set; }
        public ExpDate() { }
        public ExpDate(string id, string idReceipt, int quantity, int quantityUsed, Product product, DateTime mfgDate, DateTime expDates)
        {
            Id = id;
            IdReceipt = idReceipt;
            Quantity = quantity;
            QuantityUsed = quantityUsed;
            this.product = product;
            MfgDate = mfgDate;
            ExpDates = expDates;
        }
    }
}