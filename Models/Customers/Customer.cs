using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class Customer
    {
        public int No {  get; set; }
        public string Name { get; set; }
        public string PhoneNumber {  get; set; }
        public string Address { get; set; }
        public double TotalScore {  get; set; }
        public Cards Cards { get; set; }    
        public List<CustomerDetail> lstDetails { get; set; }
        public Customer()
        {
            lstDetails = new List<CustomerDetail>();
        }
        public Customer(int no, string name, string phoneNumber, string address, double totalScore, Cards cards, List<CustomerDetail> lstDetails)
        {
            No = no;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            TotalScore = totalScore;
            Cards = cards;
            this.lstDetails = lstDetails;
        }
    }
}
