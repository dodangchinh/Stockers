using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class CustomerDetail
    {
        public string IdSaleSlip {  get; set; }
        public int Quantity {  get; set; }
        public double Total {  get; set; }
        public double Score {  get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public SalesSlip SalesSlip { get; set; }
        public CustomerDetail() { }
        public CustomerDetail(string idSaleSlip, SalesSlip salesSlip)
        {
            IdSaleSlip = idSaleSlip;
            SalesSlip = salesSlip;
            Quantity = salesSlip.Quantity;
            Total = salesSlip.Total;
            Score += Total * 0.001;
        }
    }
}
