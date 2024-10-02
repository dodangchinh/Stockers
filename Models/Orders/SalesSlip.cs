using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class SalesSlip
    {
        public string Id { get; set; }
        public int IdCard {  get; set; }
        public string UserName {  get; set; }
        public string NameCustomer { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Quantity {  get; set; }
        public double Total {  get; set; }
        public double Discount { get; set; }
        public double TotalPay {  get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public List<SalesSlipDetail> lstDetail { get; set; }
        public SalesSlip()
        {
            lstDetail = new List<SalesSlipDetail>();
        }
        public SalesSlip(string id, int IdCard, string userName, string NameCustomer, string PhoneNumber, string Address, double discount, List<SalesSlipDetail> lstDetail)
        {
            Id = id;
            this.IdCard = IdCard;
            UserName = userName;
            this.NameCustomer = NameCustomer;
            this.PhoneNumber = PhoneNumber;
            this.Address = Address;
            this.lstDetail = lstDetail;
            Quantity = GetQuantity();
            Total = GetTotal();
            Discount = discount;
            TotalPay = Total - Discount;
        }

        int GetQuantity()
        {
            int sum = 0;
            foreach (var item in lstDetail)
                sum += item.Quantity;
            return sum;
        }

        double GetTotal()
        {
            double sum = 0;
            foreach (var item in lstDetail)
                sum += (item.product.PriceOutput * item.Quantity) - item.Discount ;
            return sum;
        }
    }
}
