using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_QuanLyKho
{
    public class Cards
    {
        public int Id {  get; set; }
        public string NameCustomer { get; set; }
        public double Score {  get; set; }
        public DateTime CreateAt = DateTime.Now;
        public Cards() { }  
        public Cards(int id, string nameCustomer, double score)
        {
            Id = id;
            NameCustomer = nameCustomer;
            Score = score;
        }
    }
}
